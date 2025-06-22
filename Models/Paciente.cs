// -----------------------------------------------------------------------------
// Archivo: Paciente.cs
// Descripción: Representa a un paciente que necesita ser atendido en el hospital.
//              Incluye su nombre, especialidad solicitada, prioridad e ID único.
// Autor: Emilio F. & Ginger R.
// Fecha de creación: 21/06/25
// -----------------------------------------------------------------------------

namespace PokemonCenter.Models
{
    public class Paciente
    {
        public int ID { get; set; } // ID único del paciente
        public string Nombre { get; set; } // Nombre del paciente
        public Especialidad EspecialidadSolicitada { get; set; } // Especialidad que solicita
        public int Prioridad { get; set; } // Prioridad en la fila
        public bool Mutado { get; set; } = false; // Indica si fue modificado por mutación genética

        public Paciente(string nombre, Especialidad especialidadSolicitada, int prioridad, int id)
        {
            ID = id;
            Nombre = nombre;
            EspecialidadSolicitada = especialidadSolicitada;
            Prioridad = prioridad;
        }
    }
}