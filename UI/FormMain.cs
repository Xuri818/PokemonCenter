using PokemonCenter.Models;
using PokemonCenter.UI;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace PokemonCenter
{
    public partial class FormMain : Form
    {
        private Button[] botonesCrearConsultorios;
        private Panel[] panelesConsultorios;
        public static List<Consultorio> consultorios;

        private int contadorPacientes = 0;

        private List<PictureBox> spritesVisuales = new List<PictureBox>();
        private Dictionary<string, Rectangle> atlas = new(); // Cargar desde JSON
        private Image atlasImage; // SpritesPokemon.png

        private System.Windows.Forms.Timer timerSimulacion;
        private bool simulacionActiva = false;
        private int minutosTranscurridos = 0;
        private double minutosRestantesAtencion = 0;



        private ToolTip toolTipSprites = new ToolTip();
        private ToolTip tooltipConsultorios = new ToolTip();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Timer
            timerSimulacion = new System.Windows.Forms.Timer();
            timerSimulacion.Interval = 1_000;
            timerSimulacion.Tick += TimerSimulacion_Tick;

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

            consultorios = new List<Consultorio>();
            for (int i = 0; i < 15; i++)
                consultorios.Add(new Consultorio(i + 1));


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
                int index = i;

                Button btn = new Button
                {
                    Text = "Crear Consultorio",
                    Dock = DockStyle.Fill,
                    Tag = index,
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    BackColor = Color.White
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
                tooltipConsultorios = new ToolTip
                {
                    AutoPopDelay = 10000,
                    InitialDelay = 200,
                    ReshowDelay = 200,
                    ShowAlways = true
                };

            }
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
            // Evento click referenciado con ID
            int id = consultorio.ID;
            btnAbrirCerrar.Click += (s, e) => CambiarEstadoConsultorio(panel, id);


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
                EliminarConsultorio(panel, id);
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
            consultorio.LabelEstado = estado;

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
            // Asignar tooltip al panel completo
            tooltipConsultorios.SetToolTip(panel, GenerarTextoTooltip(consultorio));

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

                // Redibujar fila general con sprites organizados
                DibujarSpritesFilaGeneral();
            }
        }

        private async void ButtonFilaConsultorio_Click(object sender, EventArgs e)
        {
            buttonFilaConsultorio.Enabled = false;

            var mejor = await Task.Run(() =>
            {
                return AlgoritmoGenetico.MezclarFilasConsultorios();
            });
            // Aplicar el mejor individuo al sistema
            CargarIndividuo(mejor);

            buttonFilaConsultorio.Enabled = true;
            labelTiempoAtencion.Text = $"Se termina de atender en: {mejor.Tiempo} min";
            minutosRestantesAtencion = mejor.Tiempo;
        }


        private async void ButtonFilaGeneral_Click(object sender, EventArgs e)
        {
            buttonFilaGeneral.Enabled = false;

            var mejor = await Task.Run(() =>
            {
                return AlgoritmoGenetico.IngresarFilaGeneral();
            });

            FilaGeneral.PacientesEnEspera.Clear();
            CargarIndividuo(mejor);

            buttonFilaGeneral.Enabled = true;
            labelTiempoAtencion.Text = $"Se termina de atender en: {mejor.Tiempo} min";
            minutosRestantesAtencion = mejor.Tiempo;
        }


        private void CargarIndividuo(Individuo nuevoIndividuo)
        {
            // Paso 1: LIMPIAR visualmente todas las filas de consultorios
            foreach (var panel in panelesConsultorios)
            {
                if (panel == null) continue;

                foreach (Control ctrl in panel.Controls.OfType<PictureBox>().Where(p => string.IsNullOrEmpty(p.Name)).ToList())
                {
                    panel.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }
            }

            // Paso 2: Limpiar sprites y fila general visual
            foreach (Control ctrl in panelRecepcion.Controls.OfType<PictureBox>().Where(p => string.IsNullOrEmpty(p.Name)).ToList())
            {
                panelRecepcion.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            spritesVisuales.Clear();

            for (int i = 0; i < consultorios.Count; i++)
            {
                if (i >= nuevoIndividuo.Consultorios.Count || nuevoIndividuo.Consultorios[i] == null)
                {
                    consultorios[i] = null;
                    continue;
                }

                var nuevo = nuevoIndividuo.Consultorios[i];
                var actual = consultorios[i];

                if (actual == null)
                {
                    consultorios[i] = new Consultorio(nuevo.ID, new List<Especialidad>(nuevo.Especialidades), false)
                    {
                        Activo = nuevo.Activo,
                        Ocupado = nuevo.Ocupado,
                        Fila = new List<Paciente>(nuevo.Fila)
                    };
                }
                else
                {
                    actual.Activo = nuevo.Activo;
                    actual.Ocupado = nuevo.Ocupado;
                    actual.Fila = new List<Paciente>(nuevo.Fila);
                    actual.Especialidades = new List<Especialidad>(nuevo.Especialidades);
                }
            }



            // Paso 4: Cargar pacientes no atendidos
            FilaGeneral.PacientesEnEspera.AddRange(nuevoIndividuo.PacientesNoAtendidos);

            // Paso 5: Redibujar visualmente
            ActualizarVisualizacionFilas();
        }


        private void CambiarEstadoConsultorio(Panel panel, int consultorioId)
        {
            var consultorio = consultorios[consultorioId - 1];
            consultorio.Activo = !consultorio.Activo;
            // Redibujar el consultorio con su estado actualizado
            MostrarConsultorio(panel, consultorio);

            DibujarSpritesConsultorios();
        }

        private void EliminarConsultorio(Panel panel, int consultorioId)
        {
            // 1. Obtener el consultorio real
            var consultorio = consultorios[consultorioId - 1];
            if (consultorio == null) return;

            // 2. Mover sus pacientes al final de la fila general
            if (consultorio.Fila.Any())
            {
                FilaGeneral.PacientesEnEspera.AddRange(consultorio.Fila);
                consultorio.Fila.Clear();
            }

            // 3. Eliminar el consultorio lógicamente
            consultorios[consultorioId - 1] = null;

            // 4. Limpiar visualmente el panel
            panel.Controls.Clear();

            // 5. Volver a mostrar el botón "Crear Consultorio"
            if (consultorioId - 1 < botonesCrearConsultorios.Length)
            {
                panel.Controls.Add(botonesCrearConsultorios[consultorioId - 1]);
            }

            // 6. Actualizar visualmente fila general
            ActualizarVisualizacionFilas();

            // 7. Confirmación
            MessageBox.Show($"Consultorio {consultorioId} eliminado. Sus pacientes han sido redirigidos a la fila general.");
        }
        private void ActualizarEstadoVisualConsultorio(Consultorio consultorio)
        {
            if (consultorio.LabelEstado == null)
            {
                MessageBox.Show($"Consultorio {consultorio.ID} no tiene LabelEstado");
                return;
            }

            consultorio.LabelEstado.Text = consultorio.Ocupado ? "Ocupado" : "Disponible";
            consultorio.LabelEstado.ForeColor = consultorio.Ocupado ? Color.Red : Color.Green;
            tooltipConsultorios.SetToolTip(panelesConsultorios[consultorio.ID - 1], GenerarTextoTooltip(consultorio));
        }




        private void ActualizarVisualizacionFilas()
        {
            DibujarSpritesFilaGeneral();
            DibujarSpritesConsultorios();
        }

        private void DibujarSpritesFilaGeneral()
        {
            // LIMPIAR solo los PictureBox sin nombre (sprites) de la fila general
            foreach (Control ctrl in panelRecepcion.Controls.OfType<PictureBox>().Where(p => string.IsNullOrEmpty(p.Name)).ToList())
            {
                panelRecepcion.Controls.Remove(ctrl);
                ctrl.Dispose();
            }

            spritesVisuales.RemoveAll(p => panelRecepcion.Controls.Contains(p));

            int margin = 5;
            int offsetX = 300;
            int offsetY = (panelRecepcion.Height - 32) / 2 + 10;

            foreach (var paciente in FilaGeneral.PacientesEnEspera)
            {
                string key = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(paciente.Nombre.ToLower());
                Rectangle area = atlas.ContainsKey(key) ? atlas[key] : atlas["Ditto"];

                Bitmap recorte = new Bitmap(32, 32, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(recorte))
                {
                    g.DrawImage(atlasImage, new Rectangle(0, 0, 32, 32), area, GraphicsUnit.Pixel);
                }

                PictureBox pb = new PictureBox
                {
                    BackColor = Color.Transparent,
                    Size = new Size(32, 32),
                    Location = new Point(offsetX, offsetY),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = recorte
                };

                // Agregar tooltip con información del paciente
                toolTipSprites.SetToolTip(pb,
                    $"Nombre: {paciente.Nombre}\n" +
                    $"Especialidad: {paciente.EspecialidadSolicitada.Nombre}\n" +
                    $"ID: {paciente.ID}\n" +
                    $"Prioridad: {paciente.Prioridad}\n" +
                    $"Mutado: {(paciente.Mutado ? "Sí" : "No")}"
                );
                panelRecepcion.Controls.Add(pb);
                spritesVisuales.Add(pb);

                offsetX += 32 + margin;
            }
        }

        private void DibujarSpritesConsultorios()
        {
            for (int i = 0; i < consultorios.Count; i++)
            {
                var consultorio = consultorios[i];
                if (consultorio == null) continue;

                var panel = panelesConsultorios[i];

                // Eliminar solo los sprites (PictureBox sin nombre)
                foreach (Control ctrl in panel.Controls.OfType<PictureBox>().Where(p => string.IsNullOrEmpty(p.Name)).ToList())
                {
                    panel.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }

                int offsetYSprite = 185;
                int margen = 5;

                foreach (var paciente in consultorio.Fila)
                {
                    string key = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(paciente.Nombre.ToLower());
                    Rectangle area = atlas.ContainsKey(key) ? atlas[key] : atlas["Ditto"];

                    Bitmap recorte = new Bitmap(32, 32, PixelFormat.Format32bppArgb);
                    using (Graphics g = Graphics.FromImage(recorte))
                    {
                        g.DrawImage(atlasImage, new Rectangle(0, 0, 32, 32), area, GraphicsUnit.Pixel);
                    }

                    PictureBox pb = new PictureBox
                    {
                        BackColor = Color.Transparent,
                        Size = new Size(32, 32),
                        Location = new Point((panel.Width - 32) / 2, offsetYSprite),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Image = recorte
                    };

                    toolTipSprites.SetToolTip(pb,
                        $"Nombre: {paciente.Nombre}\n" +
                        $"Especialidad: {paciente.EspecialidadSolicitada.Nombre}\n" +
                        $"ID: {paciente.ID}\n" +
                        $"Prioridad: {paciente.Prioridad}\n" +
                        $"Mutado: {(paciente.Mutado ? "Sí" : "No")}"
                    );

                    panel.Controls.Add(pb);
                    pb.BringToFront();
                    spritesVisuales.Add(pb);

                    offsetYSprite += 32 + margen;
                }
            }
        }


        private void TimerSimulacion_Tick(object sender, EventArgs e)
        {
            minutosTranscurridos++;
            labelTiempo.Text = $"Tiempo: {minutosTranscurridos} min";

            if (minutosRestantesAtencion > 0)
            {
                minutosRestantesAtencion--;
                labelTiempoAtencion.Text = $"Se termina de atender en: {minutosRestantesAtencion} min";
            }
            else
            {
                labelTiempoAtencion.Text = "Todos atendidos";
            }

            // Aumentar prioridad a pacientes en fila general
            foreach (var paciente in FilaGeneral.PacientesEnEspera)
            {
                paciente.Prioridad++;
            }

            // Aumentar prioridad a pacientes en las filas de los consultorios
            foreach (var consultorio in consultorios)
            {
                if (consultorio == null || !consultorio.Activo) continue;

                foreach (var paciente in consultorio.Fila)
                {
                    paciente.Prioridad++;
                }
            }

            // Procesar atención en cada consultorio
            foreach (var consultorio in consultorios)
            {
                if (consultorio == null || !consultorio.Activo) continue;

                if (consultorio.Ocupado && consultorio.TiempoRestanteAtencion > 0)
                {
                    consultorio.TiempoRestanteAtencion--;
                    ActualizarTooltipConsultorio(consultorio);
                }

                if (!consultorio.Ocupado && consultorio.Fila.Any())
                {
                    var paciente = consultorio.Fila.First();
                    consultorio.Fila.RemoveAt(0);

                    consultorio.Ocupado = true;
                    consultorio.TiempoRestanteAtencion = paciente.EspecialidadSolicitada.TiempoAtencion;

                    this.Invoke(() => ActualizarEstadoVisualConsultorio(consultorio));

                    int duracionMs = paciente.EspecialidadSolicitada.TiempoAtencion * 1_000;

                    // Simular tiempo de atención
                    Task.Run(async () =>
                    {
                        await Task.Delay(duracionMs);

                        // Finaliza la atención
                        this.Invoke(() =>
                        {
                            consultorio.Ocupado = false;
                            consultorio.TiempoRestanteAtencion = 0;
                            ActualizarEstadoVisualConsultorio(consultorio);
                            ActualizarTooltipConsultorio(consultorio);
                        });
                        consultorio.TiempoRestanteAtencion = 0;
                    });
                }
            }
            // Actualizar UI visualmente
            ActualizarVisualizacionFilas();
        }

        private void IniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!simulacionActiva)
            {
                timerSimulacion.Start();
                simulacionActiva = true;
            }
        }

        private void PausarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (simulacionActiva)
            {
                timerSimulacion.Stop();
                simulacionActiva = false;
            }
        }

        private void ReiniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerSimulacion.Stop();
            simulacionActiva = false;
            minutosTranscurridos = 0;

            // Limpiar filas y consultorios
            FilaGeneral.PacientesEnEspera.Clear();
            foreach (var consultorio in consultorios)
            {
                if (consultorio != null)
                {
                    consultorio.Fila.Clear();
                    consultorio.Ocupado = false;
                }
            }

            ActualizarVisualizacionFilas();
        }

        // Metodos Auxiliares
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

        private string GenerarTextoTooltip(Consultorio consultorio)
        {
            if (consultorio == null) return "Consultorio no asignado";

            string estado = consultorio.Activo ? "Abierto" : "Cerrado";
            string ocupado = consultorio.Ocupado ? "Ocupado" : "Disponible";

            string especialidades = consultorio.Especialidades.Any()
                ? string.Join(", ", consultorio.Especialidades.Select(e => e.Nombre))
                : "Ninguna";

            string fila = consultorio.Fila.Any()
                ? string.Join("\n  - ", consultorio.Fila.Select(p => $"{p.Nombre} (Prioridad {p.Prioridad})"))
                : "Vacía";

            string tiempo = consultorio.Ocupado
                ? $"{consultorio.TiempoRestanteAtencion} min restantes"
                : "—";

            return $"Consultorio {consultorio.ID}\n" +
                   $"Estado: {estado}\n" +
                   $"Ocupación: {ocupado}\n" +
                   $"Tiempo actual: {tiempo}\n" +
                   $"Especialidades: {especialidades}\n" +
                   $"Fila:\n  - {fila}";
        }

        private void ActualizarTooltipConsultorio(Consultorio consultorio)
        {
            if (consultorio == null || consultorio.ID < 1 || consultorio.ID > panelesConsultorios.Length) return;
            var panel = panelesConsultorios[consultorio.ID - 1];
            tooltipConsultorios.SetToolTip(panel, GenerarTextoTooltip(consultorio));
        }

    }

}

