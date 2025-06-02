namespace PokemonCenter.Models
{
    public class Especialidad(string nombre, int duracion)
    {
        public string Nombre { get; set; } = nombre;
        public int Duracion { get; set; } = duracion;
    }
}

