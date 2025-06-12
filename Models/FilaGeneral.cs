namespace PokemonCenter.Models
{
    public class FilaGeneral
    {
        public static List<Paciente> PacientesEnEspera { get; set; } = [];
        public static void AgregarPaciente(Paciente paciente)
        {
            PacientesEnEspera.Add(paciente);
        }
    }
}

