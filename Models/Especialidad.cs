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
        public static List<Especialidad> Todas { get; } = new()
    {
        new Especialidad("dormido", 30),
        new Especialidad("envenenado", 25),
        new Especialidad("paralizado", 20),
        new Especialidad("quemado", 35),
        new Especialidad("congelado", 20),
        new Especialidad("confundido", 40),
    };

public Especialidad(string nombre, int tiempoAtencion)
        {
            Nombre = nombre;
            TiempoAtencion = tiempoAtencion;
        }
    }
}