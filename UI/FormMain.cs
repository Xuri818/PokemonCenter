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

        private static Bitmap CambiarOpacidad(Bitmap img, float opacidad)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacidad;
                ImageAttributes attr = new ImageAttributes();
                attr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, attr);
            }
            return bmp;
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

            // Título
            Label titulo = new Label
            {
                Text = $"C {consultorio.ID}",
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Height = 20,
                BackColor = Color.Transparent
            };

            // Botón cerrar
            Button btnAbrirCerrar = new()
            {
                Text = consultorio.Activo ? "Cerrar" : "Abrir",
                Height = 20,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.FromArgb(180, Color.White)
            };
            btnAbrirCerrar.Click += (s, e) =>
            {
                consultorio.Activo = !consultorio.Activo;
                btnAbrirCerrar.Text = consultorio.Activo ? "Cerrar" : "Abrir";
                MostrarConsultorio(panel, consultorio); // Recarga visual
            };

            // Botón eliminar
            Button btnEliminar = new()
            {
                Text = "Eliminar",
                Height = 20,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Popup,
                BackColor = Color.FromArgb(180, Color.White)
            };
            btnEliminar.Click += (s, e) =>
            {
                panel.Controls.Clear();
                panel.Controls.Add(botonesCrearConsultorios[consultorio.ID - 1]);
            };

            // Colores y nombres de especialidades
            Dictionary<string, (Color color, int fila, int columna)> luces = new()
            {
                { "paralizado", (Color.Yellow, 0, 0) },
                { "envenenado", (Color.Purple, 0, 1) },
                { "dormido", (Color.Gray, 0, 2) },
                { "quemado", (Color.Red, 1, 0) },
                { "congelado", (Color.Cyan, 1, 1) },
                { "confundido", (Color.Green, 1, 2) }
            };

            TableLayoutPanel matrizLuces = new()
            {
                ColumnCount = 3,
                RowCount = 2,
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.Transparent
            };
            matrizLuces.Name = "panelLuces";
            for (int i = 0; i < 3; i++) matrizLuces.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));
            for (int i = 0; i < 2; i++) matrizLuces.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            foreach (var item in luces)
            {
                Panel luz = new()
                {
                    Width = 18,
                    Height = 18,
                    Margin = new Padding(5),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };

                bool activa = consultorio.Especialidades.Any(e => e.Nombre.ToLower() == item.Key);
                if (activa)
                    luz.BackColor = item.Value.color;

                matrizLuces.Controls.Add(luz, item.Value.columna, item.Value.fila);
            }

            // Imagen consultorio
            PictureBox imagenConsultorio = new()
            {
                Image = Image.FromFile(Path.Combine(pathBase, "Consultorio.png")),
                SizeMode = PictureBoxSizeMode.Zoom,
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.Transparent
            };
            imagenConsultorio.Name = "imgConsultorio";

            if (!consultorio.Activo)
                imagenConsultorio.Image = CambiarOpacidad((Bitmap)imagenConsultorio.Image, 0.3f);

            // Indicador de ocupado/disponible
            Label estado = new()
            {
                Text = consultorio.Ocupado ? "Ocupado" : "Disponible",
                ForeColor = consultorio.Ocupado ? Color.Red : Color.Green,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 9, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 20,
                BackColor = Color.Transparent
            };

            // Fondo si está cerrado
            panel.BackColor = consultorio.Activo ? SystemColors.Control : Color.LightGray;

            // Agregar todo al panel
            panel.BackgroundImage = Image.FromFile(Path.Combine(pathBase, "filaConsultorio.png"));
            panel.BackgroundImageLayout = ImageLayout.Stretch;
            panel.Controls.AddRange(
            [
                estado,
                imagenConsultorio,
                matrizLuces,
                btnEliminar,
                btnAbrirCerrar,
                titulo
            ]);
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

                FilaGeneral.AgregarPaciente(nuevoPaciente);
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

        private void panel1_Paint_3(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Consultorios activos
                var consultoriosAbiertos = Consultorio.Todos.Where(c => c.Activo).ToList();

                // 2. Obtener pacientes de fila general
                var pacientes = new List<Paciente>(FilaGeneral.PacientesEnEspera);

                // 3. Agregar pacientes en fila de cada consultorio
                foreach (var c in consultoriosAbiertos)
                {
                    pacientes.AddRange(c.Fila);
                    c.Fila.Clear(); // Vaciar las filas para re-optimizar
                }

                // 4. Validar si hay suficientes pacientes
                if (pacientes.Count <= 1)
                {
                    DebugLog("Muy pocos pacientes para aplicar el algoritmo genético.");
                    return;
                }

                // 5. Ejecutar algoritmo genético
                var mejor = AlgoritmoGenetico.EjecutarAlgoritmoGenetico(consultoriosAbiertos, pacientes, incluirFilaGeneral: true);
                DebugIndividuo(mejor);
                // 6. Actualizar filas de consultorios con las nuevas filas del mejor individuo
                for (int i = 0; i < consultoriosAbiertos.Count; i++)
                {
                    consultoriosAbiertos[i].Fila = mejor.Consultorios[i].Fila;
                }

                // 7. Vaciar y actualizar fila general con los no asignados
                FilaGeneral.PacientesEnEspera.Clear();
                FilaGeneral.PacientesEnEspera.AddRange(mejor.PacientesNoAtendidos);

                // 8. Refrescar interfaz
                labelTiempo.Text = $"Tiempo estimado: {mejor.Fitness} minutos";
                ActualizarVistasPacientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error durante la ejecución: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ButtonFilaConsultorio_Click(object sender, EventArgs e)
        {
            // 1. Obtener todos los pacientes de consultorios abiertos
            List<Paciente> pacientes = new();
            List<Consultorio> consultoriosAbiertos = new();

            foreach (var consultorio in Consultorio.Todos)
            {
                if (consultorio.Activo)
                {
                    pacientes.AddRange(consultorio.Fila);
                    consultoriosAbiertos.Add(consultorio);
                }

                // Vaciar todas las filas (también de cerrados)
                consultorio.Fila.Clear();
            }

            // 2. Ejecutar algoritmo genético solo con consultorios abiertos
            DebugLog("Iniciando ejecución del algoritmo genético");
            var mejorIndividuo = AlgoritmoGenetico.EjecutarAlgoritmoGenetico(consultoriosAbiertos, pacientes, false);
            DebugLog($"Algoritmo finalizado. Fitness: {mejorIndividuo.Fitness}");
            // 3. Actualizar las filas visuales de cada consultorio
            for (int i = 0; i < consultoriosAbiertos.Count; i++)
            {
                Consultorio c = consultoriosAbiertos[i];
                MostrarConsultorio(panelesConsultorios[c.ID - 1], c);
            }

            // 4. Mostrar fitness como tiempo en el label
            labelTiempo.Text = $"Tiempo estimado: {mejorIndividuo.Fitness} minutos";

            // Actualizar visualizacion de los sprites
            ActualizarVistasPacientes();
        }


        private void ActualizarVistasPacientes()
        {
            // Limpia sprites de fila general
            foreach (var sprite in spritesVisuales)
                panelRecepcion.Controls.Remove(sprite);
            spritesVisuales.Clear();

            // Limpia sprites de cada consultorio
            foreach (var panel in panelesConsultorios)
            {
                var spritesEnConsultorio = panel.Controls
                    .Cast<Control>()
                    .Where(c => c is PictureBox pic && pic.Tag?.ToString() == "PacienteSprite")
                    .ToList();

                foreach (var control in spritesEnConsultorio)
                    panel.Controls.Remove(control);
            }

            // Muestra sprites en fila general
            int offsetX = 300;
            int offsetY = (panelRecepcion.Height - 32) / 2 + 10;
            int marginX = 5;

            foreach (var paciente in FilaGeneral.PacientesEnEspera)
            {
                PictureBox pb = CrearSpritePaciente(paciente);
                pb.Location = new Point(offsetX, offsetY);
                pb.Tag = "PacienteSprite";
                panelRecepcion.Controls.Add(pb);
                spritesVisuales.Add(pb);

                offsetX += 32 + marginX;
            }

            // Muestra sprites en consultorios abiertos
            foreach (var consultorio in Consultorio.Todos)
            {
                if (!consultorio.Activo)
                    continue;

                Panel panel = panelesConsultorios[consultorio.ID - 1];

                int startY = 110;
                int centerX = (panel.Width - 32) / 2;

                foreach (var paciente in consultorio.Fila)
                {
                    PictureBox pb = CrearSpritePaciente(paciente);
                    pb.Location = new Point(centerX, startY);
                    pb.Tag = "PacienteSprite";
                    panel.Controls.Add(pb);
                    startY += 32 + 4;
                }
            }
        }


        private PictureBox CrearSpritePaciente(Paciente paciente)
        {
            string key = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(paciente.Nombre.ToLower());

            Rectangle area = atlas.ContainsKey(key) ? atlas[key] : atlas["Ditto"];
            Bitmap recorte = new(32, 32, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(recorte))
                g.DrawImage(atlasImage, new Rectangle(0, 0, 32, 32), area, GraphicsUnit.Pixel);

            return new PictureBox
            {
                BackColor = Color.Transparent,
                Size = new Size(32, 32),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = recorte
            };
        }

        private void DebugLog(string mensaje)
        {
            Console.WriteLine($"[DEBUG] {mensaje}");
            // También puedes mostrarlo en pantalla si lo deseas
            MessageBox.Show(mensaje, "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void DebugIndividuo(Individuo ind)
        {
            Console.WriteLine("----- Debug del Individuo -----");

            HashSet<int> pacientesGlobal = new();
            bool duplicados = false;

            for (int i = 0; i < ind.Consultorios.Count; i++)
            {
                var consultorio = ind.Consultorios[i];
                Console.WriteLine($"Consultorio {consultorio.ID}:");

                HashSet<int> idsLocales = new();

                foreach (var paciente in consultorio.Fila)
                {
                    Console.WriteLine($"  Paciente ID: {paciente.ID}, Nombre: {paciente.Nombre}");

                    // Verificación local (duplicado en la misma fila)
                    if (!idsLocales.Add(paciente.ID))
                    {
                        Console.WriteLine($"    ❌ Duplicado en misma fila del consultorio {consultorio.ID}: ID {paciente.ID}");
                        duplicados = true;
                    }

                    // Verificación global (duplicado en más de un consultorio)
                    if (!pacientesGlobal.Add(paciente.ID))
                    {
                        Console.WriteLine($"    ❌ Duplicado global en otro consultorio: ID {paciente.ID}");
                        duplicados = true;
                    }
                }
            }

            Console.WriteLine($"Pacientes no asignados (fila general): {ind.PacientesNoAtendidos.Count}");
            foreach (var paciente in ind.PacientesNoAtendidos)
            {
                Console.WriteLine($"  [No asignado] ID: {paciente.ID}, Nombre: {paciente.Nombre}");

                if (!pacientesGlobal.Add(paciente.ID))
                {
                    Console.WriteLine($"    ❌ Duplicado global en fila general: ID {paciente.ID}");
                    duplicados = true;
                }
            }

            if (!duplicados)
                Console.WriteLine("✅ No se encontraron pacientes duplicados.");
            else
                Console.WriteLine("⚠️ Se detectaron pacientes duplicados.");

            Console.WriteLine("--------------------------------");
        }


    }


}
