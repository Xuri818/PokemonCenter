namespace PokemonCenter.Models
{
    internal class Paciente(string nombre, Especialidad especialidadSolicitada, int prioridad)
    {
        public string Nombre { get; set; } = nombre;
        public Especialidad EspecialidadSolicitada { get; set; } = especialidadSolicitada;
        public int Prioridad { get; set; } = prioridad;
    }
}

