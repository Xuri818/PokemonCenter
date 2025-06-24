// -----------------------------------------------------------------------------
// Archivo: AlgoritmoGenetico.cs
// Descripción: Implementa la lógica del algoritmo genético para asignar pacientes.
//              Usa la fila general o las filas actuales para generar soluciones.
// Autor: Emilio F. & Ginger R.
// Fecha de creación: 21/06/25
// -----------------------------------------------------------------------------

namespace PokemonCenter.Models
{
    public class AlgoritmoGenetico
    {
        public List<Individuo> Poblacion { get; set; } = []; // Lista de individuos generados
        public static double TasaMutacion { get; set; } = 0.0000001; // Probabilidad de mutación

        // Crea un individuo con el estado actual
        public static Individuo CrearIndividuoActual()
        {
            Individuo individuoActual = new(FormMain.consultorios, FilaGeneral.PacientesEnEspera, 0);
            Fitness(individuoActual);
            return individuoActual;
        }

        // Distribuye pacientes de la fila general a los consultorios
        public static Individuo IngresarFilaGeneral(int tamPoblacion = 10)
        {
            Random rnd = new();
            List<Individuo> poblacion = [];

            var baseIndividuo = CrearIndividuoActual();

            for (int i = 0; i < tamPoblacion; i++)
            {
                var consultoriosClonados = new List<Consultorio>();

                foreach (var c in baseIndividuo.Consultorios)
                {
                    if (c == null)
                    {
                        consultoriosClonados.Add(null);
                    }
                    else
                    {
                        // Crea una nueva instancia del consultorio con los mismos valores
                        var nuevoConsultorio = new Consultorio(
                            c.ID,                        
                            new List<Especialidad>(c.Especialidades),
                            false                              
                        );

                        nuevoConsultorio.Fila = new List<Paciente>(c.Fila);
                        nuevoConsultorio.Activo = c.Activo;              
                        nuevoConsultorio.Ocupado = c.Ocupado;               
                        nuevoConsultorio.TiempoRestanteAtencion = c.TiempoRestanteAtencion; 

                        consultoriosClonados.Add(nuevoConsultorio);
                    }
                }

                List<Paciente> noAtendidos = [];

                foreach (var paciente in FilaGeneral.PacientesEnEspera)
                {
                    if (!paciente.Mutado && rnd.NextDouble() < TasaMutacion)
                        paciente.Mutado = true;
                    var compatibles = new List<Consultorio>();

                    foreach (var c in consultoriosClonados)
                    {
                        // Verifica que el consultorio no sea nulo
                        if (c == null)
                            continue;

                        // Verifica que el consultorio esté activo
                        if (!c.Activo)
                            continue;

                        // Verifica que la lista de especialidades no sea nula ni vacía
                        if (c.Especialidades == null || c.Especialidades.Count == 0)
                            continue;

                        // Si el paciente está mutado o el consultorio tiene la especialidad requerida
                        if (paciente.Mutado || c.TieneEspecialidad(paciente.EspecialidadSolicitada))
                        {
                            compatibles.Add(c);
                        }
                    }

                    if (compatibles.Count == 0)
                        noAtendidos.Add(paciente);
                    else
                        compatibles[rnd.Next(compatibles.Count)].Fila.Add(paciente);
                }

                var individuo = new Individuo(consultoriosClonados, noAtendidos, 0);
                individuo.Fitness = Fitness(individuo);
                poblacion.Add(individuo);
            }

            QuickSortIndividuos(poblacion, 0, poblacion.Count - 1);
            return poblacion[0];

        }

        // Reorganiza pacientes que ya están en consultorios
        public static Individuo MezclarFilasConsultorios(int tamPoblacion = 10)
        {
            Random rnd = new();
            List<Individuo> poblacion = [];

            var baseConsultorios = new List<Consultorio>();

            foreach (var c in FormMain.consultorios)
            {
                if (c == null)
                {
                    baseConsultorios.Add(null);
                }
                else
                {
                    // Crea un nuevo consultorio con los datos clonados del original
                    var nuevoConsultorio = new Consultorio(
                        c.ID,
                        new List<Especialidad>(c.Especialidades),
                        false // El nuevo consultorio se inicializa como no ocupado
                    );

                    // Copiar atributos adicionales
                    nuevoConsultorio.Fila = new List<Paciente>(c.Fila);
                    nuevoConsultorio.Activo = c.Activo;
                    nuevoConsultorio.Ocupado = c.Ocupado;
                    nuevoConsultorio.TiempoRestanteAtencion = c.TiempoRestanteAtencion;

                    baseConsultorios.Add(nuevoConsultorio);
                }
            }

            List<Paciente> todosPacientes = [];
            foreach (var c in FormMain.consultorios)
                if (c != null)
                    todosPacientes.AddRange(c.Fila);

            for (int i = 0; i < tamPoblacion; i++)
            {
                var consultoriosClonados = new List<Consultorio>();

                foreach (var c in baseConsultorios)
                {
                    if (c == null)
                    {
                        consultoriosClonados.Add(null);
                    }
                    else
                    {
                        // Crea una nueva instancia de Consultorio con la información copiada
                        var nuevoConsultorio = new Consultorio(
                            c.ID,
                            new List<Especialidad>(c.Especialidades),
                            false // No ocupado al clonarse
                        );

                        // Fila vacía
                        nuevoConsultorio.Fila = new List<Paciente>();

                        // Copia los demás atributos
                        nuevoConsultorio.Activo = c.Activo;
                        nuevoConsultorio.Ocupado = c.Ocupado;
                        nuevoConsultorio.TiempoRestanteAtencion = c.TiempoRestanteAtencion;

                        consultoriosClonados.Add(nuevoConsultorio);
                    }
                }

                List<Paciente> noAtendidos = [];

                foreach (var paciente in todosPacientes)
                {
                    if (!paciente.Mutado && rnd.NextDouble() < TasaMutacion)
                        paciente.Mutado = true;
                    var compatibles = new List<Consultorio>();

                    foreach (var c in consultoriosClonados)
                    {
                        // Verifica que el consultorio no sea nulo
                        if (c == null)
                            continue;

                        // Verifica que el consultorio esté activo
                        if (!c.Activo)
                            continue;

                        // Verifica que tenga una lista válida de especialidades
                        if (c.Especialidades == null || c.Especialidades.Count == 0)
                            continue;

                        // Verifica si el paciente está mutado (puede ir a cualquiera),
                        // o si el consultorio tiene la especialidad solicitada
                        if (paciente.Mutado || c.TieneEspecialidad(paciente.EspecialidadSolicitada))
                        {
                            compatibles.Add(c);
                        }
                    }
                    if (compatibles.Count == 0)
                        noAtendidos.Add(paciente);
                    else
                        compatibles[rnd.Next(compatibles.Count)].Fila.Add(paciente);
                }

                var individuo = new Individuo(consultoriosClonados, noAtendidos, 0);
                individuo.Fitness = Fitness(individuo);
                poblacion.Add(individuo);
            }

            QuickSortIndividuos(poblacion, 0, poblacion.Count - 1);
            return poblacion[0];
        }

        // Calcula el fitness de un individuo
        public static double Fitness(Individuo individuo)
        {
            double maxDuracion = 0;
            int malAsignados = 0;
            int maxTiempoOcupado = 0;
            int noAtendidos = individuo.PacientesNoAtendidos?.Count ?? 0;

            foreach (var consultorio in individuo.Consultorios)
            {
                if (consultorio == null || consultorio.Fila == null)
                    continue;

                double tiempoTotal = 0;
                foreach (var paciente in consultorio.Fila)
                {
                    if (paciente == null || paciente.EspecialidadSolicitada == null)
                        continue;

                    if (consultorio.Ocupado)
                        tiempoTotal += consultorio.TiempoRestanteAtencion;
                    if (maxTiempoOcupado > consultorio.TiempoRestanteAtencion)
                        maxTiempoOcupado = consultorio.TiempoRestanteAtencion;
                    if (!paciente.Mutado && !consultorio.TieneEspecialidad(paciente.EspecialidadSolicitada))
                    {
                        malAsignados++;
                        continue;
                    }

                    tiempoTotal += paciente.EspecialidadSolicitada.TiempoAtencion;
                }

                if (tiempoTotal > maxDuracion)
                    maxDuracion = tiempoTotal;
            }

            double pesoNoAtendidos = 100;
            double pesoMalAsignados = 50;
            individuo.Tiempo = maxDuracion + maxTiempoOcupado;

            return maxDuracion + (noAtendidos * pesoNoAtendidos) + (malAsignados * pesoMalAsignados);
        }

        // Ordena la lista de individuos por fitness (no se usa actualmente)
        private static void QuickSortIndividuos(List<Individuo> lista, int izquierda, int derecha)
        {
            if (izquierda >= derecha)
                return;

            int pivoteIndex = (izquierda + derecha) / 2;
            double pivote = lista[pivoteIndex].Fitness;

            int i = izquierda;
            int j = derecha;

            while (i <= j)
            {
                while (lista[i].Fitness < pivote) i++;
                while (lista[j].Fitness > pivote) j--;

                if (i <= j)
                {
                    var temp = lista[i];
                    lista[i] = lista[j];
                    lista[j] = temp;
                    i++;
                    j--;
                }
            }

            if (izquierda < j) QuickSortIndividuos(lista, izquierda, j);
            if (i < derecha) QuickSortIndividuos(lista, i, derecha);
        }
    }
}