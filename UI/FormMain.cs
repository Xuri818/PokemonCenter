using PokemonCenter.Models;
using PokemonCenter.UI;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text.Json;


namespace PokemonCenter
{
    public partial class FormMain : Form
    {
        private Button[] botonesCrearConsultorios;
        private List<Consultorio> consultorios;
        private Panel[] panelesConsultorios;

        private int contadorPacientes = 0;
        private List<Paciente> pacientesVisuales = new List<Paciente>();
        private List<PictureBox> spritesVisuales = new List<PictureBox>();
        private FilaGeneral filaGeneral = new FilaGeneral();
        private Dictionary<string, Rectangle> atlas = new(); // Cargar desde JSON
        private Image atlasImage; // SpritesPokemon.png

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
            // Cargar atlas JSON y sprite
            string basePath = Application.StartupPath;

            string atlasImgPath = Path.Combine(basePath, "Resource", "SpritesPokemon.png");
            atlasImage = Image.FromFile(atlasImgPath);

            string atlasJsonPath = Path.Combine(basePath, "Models", "atlas.json");
            string jsonText = File.ReadAllText(atlasJsonPath);

            var parsed = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, int>>>(jsonText);

            foreach (var kvp in parsed)
            {
                var rect = new Rectangle(kvp.Value["x"], kvp.Value["y"], kvp.Value["w"], kvp.Value["h"]);
                atlas[kvp.Key] = rect;
            }

            // Inicializar lista de consultorios
            consultorios = new List<Consultorio>(new Consultorio[15]);

            string pathBase = Path.Combine(Application.StartupPath, "Resource");

            panelRecepcion.BackgroundImage = Image.FromFile(Path.Combine(pathBase, "FilaGeneral.png"));
            panelRecepcion.BackgroundImageLayout = ImageLayout.Stretch;

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

            string pathBase = Path.Combine(Application.StartupPath, "Resource");

            Label titulo = new Label
            {
                Text = $"C {consultorio.ID}",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Height = 20,
                BackColor = Color.AliceBlue
            };

            PictureBox imagenConsultorio = new PictureBox
            {
                Image = Image.FromFile(Path.Combine(pathBase, "Consultorio.png")),
                SizeMode = PictureBoxSizeMode.Zoom,
                Height = 40,
                Dock = DockStyle.Top
            };

            FlowLayoutPanel panelEspecialidades = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Height = 60,
                Dock = DockStyle.Top,
                AutoSize = true
            };

            foreach (var especialidad in consultorio.Especialidades)
            {
                PictureBox img = new PictureBox
                {
                    Size = new Size(50, 14),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Margin = new Padding(2)
                };

                string archivo = especialidad.Nombre.ToLower() switch
                {
                    "dormido" => "Dormido.png",
                    "congelado" => "Congelado.png",
                    "confundido" => "Confuso.png",
                    "envenenado" => "Envenenado.png",
                    "paralizado" => "Paralizado.png",
                    "quemado" => "Quemado.png",
                    _ => null
                };

                if (archivo != null)
                {
                    string ruta = Path.Combine(pathBase, archivo);
                    if (File.Exists(ruta))
                        img.Image = Image.FromFile(ruta);
                }

                panelEspecialidades.Controls.Add(img);
            }

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
                panel.Controls.Add(botonesCrearConsultorios[consultorio.ID - 1]);
            };

            panel.Controls.AddRange(new Control[]
            {
            btnEliminar,
            btnAbrirCerrar,
            panelEspecialidades,
            imagenConsultorio,
            titulo
            });
        }

        private void agregarPacienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormAgregarPaciente();
            if (form.ShowDialog() == DialogResult.OK)
            {
                string nombre = form.NombreSeleccionado;
                Especialidad esp = form.EspecialidadSeleccionada;
                int id = contadorPacientes + 1;
                int prioridad = 0;

                var nuevoPaciente = new Paciente(nombre, esp, prioridad, id);
                contadorPacientes++;

                filaGeneral.AgregarPaciente(nuevoPaciente);
                pacientesVisuales.Add(nuevoPaciente);

                // Obtener sprite
                string key = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nombre.ToLower());
                Rectangle area = atlas.ContainsKey(key)
                    ? atlas[key]
                    : atlas["Ditto"];

                Bitmap recorte = new Bitmap(32, 32, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(recorte))
                {
                    g.DrawImage(atlasImage, new Rectangle(0, 0, 32, 32), area, GraphicsUnit.Pixel);
                }

                // Posición en panelRecepcion
                int margin = 5;
                int offsetX = 300 + (spritesVisuales.Count * (32 + margin));
                int offsetY = (panelRecepcion.Height - 32) / 2 + 10;

                PictureBox pb = new PictureBox
                {
                    BackColor = Color.Transparent,
                    Size = new Size(32, 32),
                    Location = new Point(offsetX, offsetY),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = recorte
                };

                spritesVisuales.Add(pb);
                panelRecepcion.Controls.Add(pb);
            }
        }
    }
}
