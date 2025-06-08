using PokemonCenter.Models;
using PokemonCenter.UI;

namespace PokemonCenter
{
    public partial class FormMain : Form
    {
        private Button[] botonesCrearConsultorios;
        private List<Consultorio> consultorios;
        private Panel[] panelesConsultorios;

        public FormMain()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Inicializar lista de consultorios (vacíos al principio)
            consultorios = new List<Consultorio>(new Consultorio[15]);

            // Crear arreglo de paneles
            panelesConsultorios = new Panel[] {
                panel1, panel2, panel3, panel4, panel5,
                panel6, panel7, panel8, panel9, panel10,
                panel11, panel12, panel13, panel14, panel15
            };

            // Crear botones para cada panel
            botonesCrearConsultorios = new Button[15];

            for (int i = 0; i < 15; i++)
            {
                int index = i; // importante para el closure

                Button btn = new Button
                {
                    Text = "Crear Consultorio",
                    Dock = DockStyle.Fill,
                    Tag = index,
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold)
                };

                btn.Click += (s, e) =>
                {
                    var form = new FormCrearConsultorio(index + 1);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        List<Especialidad> especialidades = form.EspecialidadesSeleccionadas;

                        var nuevoConsultorio = new Consultorio(index + 1, especialidades);
                        consultorios[index] = nuevoConsultorio;

                        panelesConsultorios[index].Controls.Clear();
                        MostrarConsultorio(panelesConsultorios[index], nuevoConsultorio);
                    }
                };

                botonesCrearConsultorios[i] = btn;
                panelesConsultorios[i].Controls.Add(btn);
            }
        }




        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listaEspera_SelectedIndexChanged(object sender, EventArgs e)
        {

        } 
        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MostrarConsultorio(Panel panel, Consultorio consultorio)
        {
            panel.Controls.Clear();

            Label titulo = new Label
            {
                Text = $"C {consultorio.ID}",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Height = 20,
                BackColor = Color.AliceBlue
            };

            Label especialidades = new Label
            {
                Text = string.Join("\n", consultorio.Especialidades.Select(e => "- " + e.Nombre)),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial Narrow", 8),
                Height = 100
            };

            Button btnAbrirCerrar = new Button
            {
                Text = consultorio.Activo ? "Cerrar" : "Abrir",
                Dock = DockStyle.Top,
                Height = 25
            };
            btnAbrirCerrar.Click += (s, e) =>
            {
                consultorio.Activo = !consultorio.Activo;
                btnAbrirCerrar.Text = consultorio.Activo ? "Cerrar" : "Abrir";
            };

            Button btnEliminar = new Button
            {
                Text = "Eliminar",
                Dock = DockStyle.Top,
                Height = 25,
                BackColor = Color.LightCoral
            };
            btnEliminar.Click += (s, e) =>
            {
                panel.Controls.Clear();
                Button btn = new Button { Text = "Crear Consultorio", Dock = DockStyle.Fill };
                panel.Controls.Add(botonesCrearConsultorios[consultorio.ID - 1]);
                btnEliminar.Click += (s, e) =>
                {
                    panel.Controls.Clear();
                    panel.Controls.Add(botonesCrearConsultorios[consultorio.ID - 1]);
                };
                panel.Controls.Add(btn);
            };

            panel.Controls.AddRange(new Control[]
            {
        btnEliminar, btnAbrirCerrar, especialidades, titulo
            });
        }

    }
}
