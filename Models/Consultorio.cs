namespace PokemonCenter.Models
{
    public class Consultorio
    {
        public static List<Consultorio> Todos { get; } = [];

        public int ID { get; set; }
        public List<Especialidad> Especialidades { get; set; }
        public bool Activo { get; set; } = true;
        public bool Ocupado { get; set; } = false;
        public List<Paciente> Fila { get; set; } = [];

        public Consultorio(int id, List<Especialidad> especialidades)
        {
            ID = id;
            Especialidades = especialidades;
            Todos.Add(this);
        }

        public Consultorio(int id) : this(id, [])
        {
        }

        public bool TieneEspecialidad(Especialidad especialidad)
        {
            return Especialidades.Contains(especialidad);
        }
    }
}




