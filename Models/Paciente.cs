namespace PokemonCenter.Models
{
    public class Paciente(string nombre, Especialidad especialidadSolicitada, int prioridad, int id)
    {
        public int ID { get; set; } = id;
        public string Nombre { get; set; } = nombre;
        public Especialidad EspecialidadSolicitada { get; set; } = especialidadSolicitada;
        public int Prioridad { get; set; } = prioridad;
    }
}
  