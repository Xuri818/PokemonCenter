namespace PokemonCenter.Models
{
    internal class FilaGeneral
    {
        public List<Paciente> PacientesEnEspera { get; set; } = [];
        public void AgregarPaciente(Paciente paciente)
        {
            PacientesEnEspera.Add(paciente);
        }
    }
}

