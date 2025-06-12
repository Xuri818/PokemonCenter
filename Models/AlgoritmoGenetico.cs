namespace PokemonCenter.Models
{
    internal class AlgoritmoGenetico()
    {
        public List<Individuo> Poblacion { get; set; } = [];
        public static double TasaMutacion { get; set; } = 0.01;

        // Calcular Fitness
        public static double CalcularFitness(Individuo individuo)
        {
            // Almacena el tiempo total por consultorio
            List<int> tiempos = [];

            // Se recorre todos los consultorios
            foreach (var consultorio in individuo.Consultorios)
            {
                int tiempoTotal = 0;
                // Se recorre cada paciente de la fila del consultorio
                foreach (var paciente in consultorio.Fila)
                {
                    bool tieneEspecialidad = consultorio.Especialidades
                           .Any(e => e.Nombre.Equals(paciente.EspecialidadSolicitada.Nombre, StringComparison.OrdinalIgnoreCase));

                    if (tieneEspecialidad)
                        tiempoTotal += paciente.EspecialidadSolicitada.TiempoAtencion;
                    else
                        tiempoTotal += 1000;
                }
                tiempos.Add(tiempoTotal);
            }

            // Penalización por pacientes no asignados
            int penalizacion = individuo.PacientesNoAtendidos.Count * 1000;
            // El fitness es el tiempo máximo entre los consultorios
            double fitness = tiempos.Max();
            individuo.Fitness = fitness;
            return fitness;
        }

        // Cruce
        public static Individuo Cruce(Individuo padre1, Individuo padre2)
        {
            List<Consultorio> nuevosConsultorios = [];

            int mitad = padre1.Consultorios.Count / 2;

            // 1. Tomar consultorios de ambos padres
            for (int i = 0; i < padre1.Consultorios.Count; i++)
            {
                var original = i < mitad ? padre1.Consultorios[i] : padre2.Consultorios[i];
                var copia = new Consultorio(original.ID);

                // Copiar especialidades (pueden clonarse sin problema)
                foreach (var esp in original.Especialidades)
                {
                    copia.Especialidades.Add(new Especialidad(esp.Nombre, esp.TiempoAtencion));
                }

                // Copiar referencias de pacientes
                foreach (var paciente in original.Fila)
                {
                    copia.Fila.Add(paciente); // MISMO objeto paciente
                }

                nuevosConsultorios.Add(copia);
            }

            // 2. Evitar duplicados
            HashSet<int> idsAsignados = [];
            foreach (var c in nuevosConsultorios)
            {
                c.Fila = c.Fila
                    .Where(p => idsAsignados.Add(p.ID))
                    .ToList();
            }

            // 3. Obtener todos los pacientes únicos (por ID)
            var todosPacientes = padre1.Consultorios
                .SelectMany(c => c.Fila)
                .Concat(padre2.Consultorios.SelectMany(c => c.Fila))
                .GroupBy(p => p.ID)
                .Select(g => g.First())
                .ToList();

            // 4. Determinar cuáles no fueron asignados
            var pacientesNoAtendidos = todosPacientes
                .Where(p => !idsAsignados.Contains(p.ID))
                .ToList(); // Misma referencia

            return new Individuo(nuevosConsultorios, pacientesNoAtendidos, 0);
        }



        public static void Mutar(Individuo individuo)
        {
            Random rnd = new();

            foreach (var consultorio in individuo.Consultorios)
            {
                for (int i = 0; i < consultorio.Fila.Count; i++)
                {
                    Paciente paciente = consultorio.Fila[i];

                    // Obtener el nombre de la especialidad actual
                    string especialidadActual = paciente.EspecialidadSolicitada.Nombre;

                    // Crear una lista de especialidades diferentes
                    List<Especialidad> opciones = new();
                    foreach (var especialidad in Especialidad.Todas)
                    {
                        if (especialidad.Nombre != especialidadActual)
                        {
                            opciones.Add(especialidad);
                        }
                    }

                    // Si hay opciones, seleccionar una al azar y asignarla
                    if (opciones.Count > 0)
                    {
                        int indiceAleatorio = rnd.Next(opciones.Count);
                        paciente.EspecialidadSolicitada = opciones[indiceAleatorio];
                    }
                }
            }
        }

        private static List<Consultorio> ClonarConsultorios(List<Consultorio> originales)
        {
            var copia = new List<Consultorio>();

            foreach (var c in originales)
            {
                // Clonar las especialidades
                var especialidadesClonadas = new List<Especialidad>();
                foreach (var e in c.Especialidades)
                {
                    var especialidad = new Especialidad(e.Nombre, e.TiempoAtencion);
                    especialidadesClonadas.Add(especialidad);
                }

                // Crear nuevo consultorio con las especialidades clonadas
                var nuevoConsultorio = new Consultorio(c.ID, especialidadesClonadas)
                {
                    Activo = c.Activo,
                    Fila = [] 
                };

                copia.Add(nuevoConsultorio);
            }

            return copia;
        }


        public static Individuo EjecutarAlgoritmoGenetico(List<Consultorio> consultoriosOriginales, List<Paciente> pacientesDisponibles, bool incluirFilaGeneral)
        {
            int generaciones = 3;
            int tamañoPoblacion = 4;
            Random rnd = new();
            List<Individuo> poblacion = [];

            for (int i = 0; i < tamañoPoblacion; i++)
            {
                // Clona consultorios
                List<Consultorio> copiaConsultorios = ClonarConsultorios(consultoriosOriginales);

                // Usa directamente los pacientes originales
                List<Paciente> copiaPacientes = new(pacientesDisponibles);

                // Reparte pacientes entre consultorios compatibles
                List<Paciente> noAsignados = [];

                foreach (var paciente in copiaPacientes)
                {
                    var compatibles = copiaConsultorios
                        .Where(c => c.Especialidades.Any(e =>
                            e.Nombre.Equals(paciente.EspecialidadSolicitada.Nombre, StringComparison.OrdinalIgnoreCase)))
                        .ToList();

                    if (compatibles.Count == 0)
                    {
                        noAsignados.Add(paciente);
                    }
                    else
                    {
                        int idx = rnd.Next(compatibles.Count);
                        compatibles[idx].Fila.Add(paciente);
                    }
                }

                var ind = new Individuo(copiaConsultorios, noAsignados, 0);
                CalcularFitness(ind);
                poblacion.Add(ind);
            }

            // Evolución por generaciones
            for (int g = 0; g < generaciones; g++)
            {
                DebugLog($"Generación {g + 1}");
                List<Individuo> nuevaGen = [];

                while (nuevaGen.Count < tamañoPoblacion)
                {
                    int tamañoTorneo = Math.Min(5, poblacion.Count);
                    List<Individuo> seleccion = poblacion.OrderBy(x => rnd.Next()).Take(tamañoTorneo).ToList();
                    QuickSortIndividuos(seleccion, 0, seleccion.Count - 1);

                    Individuo padre1 = seleccion[0];
                    Individuo padre2 = seleccion[Math.Min(1, seleccion.Count - 1)];

                    Individuo hijo = Cruce(padre1, padre2);

                    if (rnd.NextDouble() < TasaMutacion)
                    {
                        DebugLog("Aplicando mutación");
                        Mutar(hijo);
                    }

                    CalcularFitness(hijo);
                    nuevaGen.Add(hijo);
                }

                poblacion = nuevaGen;
            }

            // Obtener el mejor individuo
            Individuo mejor = poblacion.OrderBy(i => i.Fitness).First();

            // Aplicar sus filas a los consultorios originales
            for (int i = 0; i < consultoriosOriginales.Count; i++)
            {
                consultoriosOriginales[i].Fila = mejor.Consultorios[i].Fila;
            }

            // Restura a la fila general los pacientes no asignados
            FilaGeneral.PacientesEnEspera.Clear();
            foreach (var paciente in mejor.PacientesNoAtendidos)
            {
                FilaGeneral.AgregarPaciente(paciente);
            }

            return mejor;
        }


        private static void QuickSortIndividuos(List<Individuo> lista, int izquierda, int derecha)
        {
            if (izquierda >= derecha)
                return;

            // pivote valor central
            int indicePivote = (izquierda + derecha) / 2;
            double valorPivote = lista[indicePivote].Fitness;

            int i = izquierda;
            int j = derecha;

            while (i <= j)
            {
                // Mover i hasta encontrar un valor mayor o igual al pivote
                while (lista[i].Fitness < valorPivote)
                    i++;

                // Mover j hasta encontrar un valor menor o igual al pivote
                while (lista[j].Fitness > valorPivote)
                    j--;

                if (i <= j)
                {
                    // Intercambiar los elementos en i y j
                    Individuo temp = lista[i];
                    lista[i] = lista[j];
                    lista[j] = temp;

                    i++;
                    j--;
                }
            }

            // Recursión para ordenar las dos mitades
            if (izquierda < j)
                QuickSortIndividuos(lista, izquierda, j);
            if (i < derecha)
                QuickSortIndividuos(lista, i, derecha);
        }

        private static void DebugLog(string mensaje)
        {
            Console.WriteLine($"[DEBUG] {mensaje}");
            // También puedes mostrarlo en pantalla si lo deseas
            MessageBox.Show(mensaje, "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }




}


