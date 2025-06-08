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
    public partial class FormCrearConsultorio : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Especialidad> EspecialidadesSeleccionadas { get; set; }

        public FormCrearConsultorio(int numeroConsultorio)
        {
            InitializeComponent();
            this.Text = $"Crear Consultorio {numeroConsultorio}";
            EspecialidadesSeleccionadas = new List<Especialidad>();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (chkDormido.Checked)
                EspecialidadesSeleccionadas.Add(new Especialidad("Dormido", 30));
            if (chkEnvenenado.Checked)
                EspecialidadesSeleccionadas.Add(new Especialidad("Envenenado", 25));
            if (chkParalizado.Checked)
                EspecialidadesSeleccionadas.Add(new Especialidad("Paralizado", 20));
            if (chkQuemado.Checked)
                EspecialidadesSeleccionadas.Add(new Especialidad("Quemado", 35));
            if (chkCongelado.Checked)
                EspecialidadesSeleccionadas.Add(new Especialidad("Congelado", 20));
            if (chkConfundido.Checked)
                EspecialidadesSeleccionadas.Add(new Especialidad("Confundido", 40));

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnAceptar.Click += btnAceptar_Click;
        }

        private void FormCrearConsultorio_Load(object sender, EventArgs e)
        {

        }

        private void chkDormido_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
