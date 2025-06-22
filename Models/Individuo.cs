// -----------------------------------------------------------------------------
// Archivo: Individuo.cs
// Descripción: Representa una solución (individuo) dentro del algoritmo genético.
//              Cada individuo contiene una distribución de pacientes en consultorios.
// Autor: Emilio F. & Ginger R.
// Fecha de creación: 21/06/25
// -----------------------------------------------------------------------------

namespace PokemonCenter.Models
{
    public class Individuo
    {
        public List<Consultorio> Consultorios { get; set; } // Consultorios que forman parte de este individuo
        public List<Paciente> PacientesNoAtendidos { get; set; } // Pacientes que no fueron atendidos por ningún consultorio
        public double Fitness { get; set; } // Fitness entre menos mejor
        public double Tiempo { get; set; } // Tiempo en el que todos los pacientes terminan de ser atendidos

        public Individuo(List<Consultorio> consultorios, List<Paciente> pacientesNoAtendidos, double fitness)
        {
            Consultorios = consultorios;
            PacientesNoAtendidos = pacientesNoAtendidos;
            Fitness = fitness;
            Tiempo = 0;
        }
    }
}