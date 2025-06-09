using PokemonCenter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokemonCenter.UI
{
    public partial class FormAgregarPaciente : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string NombreSeleccionado { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Especialidad EspecialidadSeleccionada { get; private set; }

        public FormAgregarPaciente()
        {
        
        InitializeComponent();
            List<string> nombresPokemon = new()
            {
                "Bulbasaur", "Ivysaur", "Venusaur", "Charmander", "Charmeleon", "Charizard",
                "Squirtle", "Wartortle", "Blastoise", "Caterpie", "Metapod", "Butterfree",
                "Weedle", "Kakuna", "Beedrill", "Pidgey", "Pidgeotto", "Pidgeot",
                "Rattata", "Raticate", "Spearow", "Fearow", "Ekans", "Arbok",
                "Pikachu", "Raichu", "Sandshrew", "Sandslash", "NidoranHembra", "Nidorina",
                "Nidoqueen", "NidoranMacho", "Nidorino", "Nidoking", "Clefairy", "Clefable",
                "Vulpix", "Ninetales", "Jigglypuff", "Wigglytuff", "Zubat", "Golbat",
                "Oddish", "Gloom", "Vileplume", "Paras", "Parasect", "Venonat",
                "Venomoth", "Diglett", "Dugtrio", "Meowth", "Persian", "Psyduck",
                "Golduck", "Mankey", "Primeape", "Growlithe", "Arcanine", "Poliwag",
                "Poliwhirl", "Poliwrath", "Abra", "Kadabra", "Alakazam", "Machop",
                "Machoke", "Machamp", "Bellsprout", "Weepinbell", "Victreebel",
                "Tentacool", "Tentacruel", "Geodude", "Graveler", "Golem", "Ponyta",
                "Rapidash", "Slowpoke", "Slowbro", "Magnemite", "Magneton", "Farfetchd",
                "Doduo", "Dodrio", "Seel", "Dewgong", "Grimer", "Muk", "Shellder",
                "Cloyster", "Gastly", "Haunter", "Gengar", "Onix", "Drowzee", "Hypno",
                "Krabby", "Kingler", "Voltorb", "Electrode", "Exeggcute", "Exeggutor",
                "Cubone", "Marowak", "Hitmonlee", "Hitmonchan", "Lickitung", "Koffing",
                "Weezing", "Rhyhorn", "Rhydon", "Chansey", "Tangela", "Kangaskhan",
                "Horsea", "Seadra", "Goldeen", "Seaking", "Staryu", "Starmie",
                "Mr. Mime", "Scyther", "Jynx", "Electabuzz", "Magmar", "Pinsir", "Tauros",
                "Magikarp", "Gyarados", "Lapras", "Ditto", "Eevee", "Vaporeon", "Jolteon",
                "Flareon", "Porygon", "Omanyte", "Omastar", "Kabuto", "Kabutops",
                "Aerodactyl", "Snorlax", "Articuno", "Zapdos", "Moltres", "Dratini",
                "Dragonair", "Dragonite", "Mewtwo", "Mew"
            };

            comboBoxNombre.Items.AddRange(nombresPokemon.ToArray());
            comboBoxNombre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxNombre.AutoCompleteSource = AutoCompleteSource.ListItems;

            // Especialidades
            comboBoxPadecimiento.Items.AddRange(new string[]
            {
            "Dormido", "Envenenado", "Paralizado", "Quemado", "Congelado", "Confundido"
            });
            comboBoxPadecimiento.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void FormAgregarPaciente_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        string nombre = comboBoxNombre.Text.Trim();
        string espNombre = comboBoxPadecimiento.SelectedItem?.ToString();

        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(espNombre))
        {
            MessageBox.Show("Por favor selecciona un nombre y una especialidad.", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int duracion = espNombre switch
        {
            "Dormido" => 30,
            "Envenenado" => 25,
            "Paralizado" => 20,
            "Quemado" => 35,
            "Congelado" => 20,
            "Confundido" => 40,
            _ => 30
        };

        NombreSeleccionado = nombre;
        EspecialidadSeleccionada = new Especialidad(espNombre, duracion);

        this.DialogResult = DialogResult.OK;
        this.Close();
        }
    }
}
