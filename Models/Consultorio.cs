namespace PokemonCenter.Models
{
    public class Consultorio
    {
        public static List<Consultorio> Todos { get; } = [];

        public int ID { get; set; }
        public List<Especialidad> Especialidades { get; set; }
        public bool Activo { get; set; } = true;
        public bool Ocupado { get; set; } = false;
        public Label LabelEstado { get; set; }
        public List<Paciente> Fila { get; set; } = [];
        public int TiempoRestanteAtencion { get; set; } = 0;

        public Consultorio(int id, List<Especialidad> especialidades) : this(id, especialidades, true)
        {
        }

        public Consultorio(int id, List<Especialidad> especialidades, bool agregarAListaGlobal)
        {
            ID = id;
            Especialidades = especialidades;
            Fila = new List<Paciente>();
            Activo = true;
            Ocupado = false;

            if (agregarAListaGlobal)
                Todos.Add(this);
        }


        public Consultorio(int id) : this(id, [])
        {
        }

        public bool TieneEspecialidad(Especialidad especialidad)
        {
            return Especialidades.Any(e => e.Nombre.Equals(especialidad.Nombre, StringComparison.OrdinalIgnoreCase));
        }
    }
}




