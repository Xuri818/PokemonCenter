// -----------------------------------------------------------------------------
// Archivo: Especialidad.cs
// Descripción: Define una especialidad médica y el tiempo que tarda en atender.
//              Se registran todas las especialidades creadas en una lista global.
// Autor: Emilio F. & Ginger R.
// Fecha de creación: 21/06/25
// -----------------------------------------------------------------------------

namespace PokemonCenter.Models
{
    public class Especialidad
    {
        public string Nombre { get; set; } // Nombre de la especialidad
        public int TiempoAtencion { get; set; } // Tiempo en minutos para atender
        public static List<Especialidad> Todas { get; } = []; // Lista global de todas las especialidades

        public Especialidad(string nombre, int tiempoAtencion)
        {
            Nombre = nombre;
            TiempoAtencion = tiempoAtencion;
            Todas.Add(this);
        }
    }
}