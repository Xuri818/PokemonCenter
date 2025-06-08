namespace PokemonCenter.Models
{
    internal class Consultorio(int id, List<Especialidad> especialidades)
    {
        public int ID { get; set; } = id;
        public List<Especialidad> Especialidades { get; set; } = especialidades;
        public bool Activo { get; set; } = true;
        public bool Ocupado { get; set; } = false;
        public  List<Paciente> Fila { get; set; } = new List<Paciente>();
        public bool TieneEspecialidad(Especialidad especialidad)
        {
            return Especialidades.Contains(especialidad);
        }
    }
}


