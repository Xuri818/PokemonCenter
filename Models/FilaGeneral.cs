// -----------------------------------------------------------------------------
// Archivo: FilaGeneral.cs
// Descripción: Representa la fila de espera general para pacientes que aún no han sido asignados.
//              Permite agregar pacientes a esta lista estática compartida.
// Autor: Emilio F. & Ginger R.
// Fecha de creación: 21/06/25
// -----------------------------------------------------------------------------

namespace PokemonCenter.Models
{
    public class FilaGeneral
    {
        public static List<Paciente> PacientesEnEspera { get; set; } = []; // Pacientes esperando por atención
        public static void AgregarPaciente(Paciente paciente) // Agrega un paciente a la fila
        {
            PacientesEnEspera.Add(paciente);
        }
    }
}