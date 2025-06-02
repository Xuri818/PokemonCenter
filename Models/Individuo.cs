namespace PokemonCenter.Models
{
    internal class Individuo(List<Consultorio> consultorios, List<Paciente> pacientesNoAtendidos, double fitness)
    {
        public List<Consultorio> Consultorios { get; set; } = consultorios;
        public List<Paciente> PacientesNoAtendidos { get; set; } = pacientesNoAtendidos;
        public double Fitness { get; set; } = fitness;
    }
}

