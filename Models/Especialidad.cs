
namespace PokemonCenter.Models
{
    public class Especialidad
    {
        public string Nombre { get; set; }
        public int TiempoAtencion { get; set; }

        // Lista estática de todas las especialidades creadas
        public static List<Especialidad> Todas { get; } = [];

        public Especialidad(string nombre, int tiempoAtencion)
        {
            Nombre = nombre;
            TiempoAtencion = tiempoAtencion;
            Todas.Add(this);
        }
    }
}

