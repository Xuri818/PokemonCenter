namespace PokemonCenter.Models
{
    internal class AlgoritmoGenetico(double tasaMutacion = 0.01)
    {
        public List<Individuo> Poblacion { get; set; } = [];
        public double TasaMutacion { get; set; } = tasaMutacion;

        // Funcion Fitness, Cruce, Mutacion
    }
}

