using PokemonCenter.Models;

namespace PokemonCenter
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            iniciarToolStripMenuItem = new ToolStripMenuItem();
            pausarToolStripMenuItem = new ToolStripMenuItem();
            reiniciarToolStripMenuItem = new ToolStripMenuItem();
            pacientesToolStripMenuItem = new ToolStripMenuItem();
            agregarPacienteToolStripMenuItem = new ToolStripMenuItem();
            cargarArchivoToolStripMenuItem = new ToolStripMenuItem();
            acercaDeToolStripMenuItem = new ToolStripMenuItem();
            panelRecepcion = new Panel();
            listaEspera = new ListBox();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            panel9 = new Panel();
            panel10 = new Panel();
            panel11 = new Panel();
            panel12 = new Panel();
            panel13 = new Panel();
            panel14 = new Panel();
            panel15 = new Panel();
            menuStrip1.SuspendLayout();
            panelRecepcion.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem, pacientesToolStripMenuItem, acercaDeToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1308, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { iniciarToolStripMenuItem, pausarToolStripMenuItem, reiniciarToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(78, 20);
            archivoToolStripMenuItem.Text = "Simulacion";
            archivoToolStripMenuItem.Click += archivoToolStripMenuItem_Click;
            // 
            // iniciarToolStripMenuItem
            // 
            iniciarToolStripMenuItem.Name = "iniciarToolStripMenuItem";
            iniciarToolStripMenuItem.Size = new Size(119, 22);
            iniciarToolStripMenuItem.Text = "Iniciar";
            // 
            // pausarToolStripMenuItem
            // 
            pausarToolStripMenuItem.Name = "pausarToolStripMenuItem";
            pausarToolStripMenuItem.Size = new Size(119, 22);
            pausarToolStripMenuItem.Text = "Pausar";
            // 
            // reiniciarToolStripMenuItem
            // 
            reiniciarToolStripMenuItem.Name = "reiniciarToolStripMenuItem";
            reiniciarToolStripMenuItem.Size = new Size(119, 22);
            reiniciarToolStripMenuItem.Text = "Reiniciar";
            // 
            // pacientesToolStripMenuItem
            // 
            pacientesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { agregarPacienteToolStripMenuItem, cargarArchivoToolStripMenuItem });
            pacientesToolStripMenuItem.Name = "pacientesToolStripMenuItem";
            pacientesToolStripMenuItem.Size = new Size(69, 20);
            pacientesToolStripMenuItem.Text = "Pacientes";
            // 
            // agregarPacienteToolStripMenuItem
            // 
            agregarPacienteToolStripMenuItem.Name = "agregarPacienteToolStripMenuItem";
            agregarPacienteToolStripMenuItem.Size = new Size(164, 22);
            agregarPacienteToolStripMenuItem.Text = "Agregar paciente";
            // 
            // cargarArchivoToolStripMenuItem
            // 
            cargarArchivoToolStripMenuItem.Name = "cargarArchivoToolStripMenuItem";
            cargarArchivoToolStripMenuItem.Size = new Size(164, 22);
            cargarArchivoToolStripMenuItem.Text = "Cargar archivo";
            // 
            // acercaDeToolStripMenuItem
            // 
            acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            acercaDeToolStripMenuItem.Size = new Size(71, 20);
            acercaDeToolStripMenuItem.Text = "Acerca de";
            // 
            // panelRecepcion
            // 
            panelRecepcion.BackColor = Color.LightGray;
            panelRecepcion.Controls.Add(listaEspera);
            panelRecepcion.Location = new Point(12, 594);
            panelRecepcion.Name = "panelRecepcion";
            panelRecepcion.Size = new Size(1284, 94);
            panelRecepcion.TabIndex = 1;
            // 
            // listaEspera
            // 
            listaEspera.Font = new Font("Arial", 10F);
            listaEspera.Location = new Point(602, 3);
            listaEspera.Name = "listaEspera";
            listaEspera.Size = new Size(679, 84);
            listaEspera.TabIndex = 0;
            listaEspera.SelectedIndexChanged += listaEspera_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightGray;
            panel1.Location = new Point(12, 27);
            panel1.Name = "panel1";
            panel1.Size = new Size(80, 561);
            panel1.TabIndex = 2;
 
            // 
            // panel2
            // 
            panel2.BackColor = Color.LightGray;
            panel2.Location = new Point(98, 27);
            panel2.Name = "panel2";
            panel2.Size = new Size(80, 561);
            panel2.TabIndex = 3;
            panel2.Paint += panel2_Paint;
            // 
            // panel3
            // 
            panel3.BackColor = Color.LightGray;
            panel3.Location = new Point(184, 27);
            panel3.Name = "panel3";
            panel3.Size = new Size(80, 561);
            panel3.TabIndex = 3;
            panel3.Paint += panel3_Paint_1;
            // 
            // panel4
            // 
            panel4.BackColor = Color.LightGray;
            panel4.Location = new Point(270, 27);
            panel4.Name = "panel4";
            panel4.Size = new Size(80, 561);
            panel4.TabIndex = 4;
            // 
            // panel5
            // 
            panel5.BackColor = Color.LightGray;
            panel5.Location = new Point(356, 27);
            panel5.Name = "panel5";
            panel5.Size = new Size(80, 561);
            panel5.TabIndex = 5;
 
            // 
            // panel6
            // 
            panel6.BackColor = Color.LightGray;
            panel6.Location = new Point(442, 27);
            panel6.Name = "panel6";
            panel6.Size = new Size(80, 561);
            panel6.TabIndex = 6;

            // 
            // panel7
            // 
            panel7.BackColor = Color.LightGray;
            panel7.Location = new Point(528, 27);
            panel7.Name = "panel7";
            panel7.Size = new Size(80, 561);
            panel7.TabIndex = 7;
 
            // 
            // panel8
            // 
            panel8.BackColor = Color.LightGray;
            panel8.Location = new Point(614, 27);
            panel8.Name = "panel8";
            panel8.Size = new Size(80, 561);
            panel8.TabIndex = 8;
      
            // 
            // panel9
            // 
            panel9.BackColor = Color.LightGray;
            panel9.Location = new Point(700, 27);
            panel9.Name = "panel9";
            panel9.Size = new Size(80, 561);
            panel9.TabIndex = 9;
   
            // 
            // panel10
            // 
            panel10.BackColor = Color.LightGray;
            panel10.Location = new Point(786, 27);
            panel10.Name = "panel10";
            panel10.Size = new Size(80, 561);
            panel10.TabIndex = 10;
  
            // 
            // panel11
            // 
            panel11.BackColor = Color.LightGray;
            panel11.Location = new Point(872, 27);
            panel11.Name = "panel11";
            panel11.Size = new Size(80, 561);
            panel11.TabIndex = 11;
            // 
            // panel12
            // 
            panel12.BackColor = Color.LightGray;
            panel12.Location = new Point(958, 27);
            panel12.Name = "panel12";
            panel12.Size = new Size(80, 561);
            panel12.TabIndex = 12;

            // 
            // panel13
            // 
            panel13.BackColor = Color.LightGray;
            panel13.Location = new Point(1044, 27);
            panel13.Name = "panel13";
            panel13.Size = new Size(80, 561);
            panel13.TabIndex = 13;

            // 
            // panel14
            // 
            panel14.BackColor = Color.LightGray;
            panel14.Location = new Point(1130, 27);
            panel14.Name = "panel14";
            panel14.Size = new Size(80, 561);
            panel14.TabIndex = 14;
            // 
            // panel15
            // 
            panel15.BackColor = Color.LightGray;
            panel15.Location = new Point(1216, 27);
            panel15.Name = "panel15";
            panel15.Size = new Size(80, 561);
            panel15.TabIndex = 15;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1308, 700);
            Controls.Add(panel15);
            Controls.Add(panel14);
            Controls.Add(panel13);
            Controls.Add(panel12);
            Controls.Add(panel11);
            Controls.Add(panel10);
            Controls.Add(panel9);
            Controls.Add(panel8);
            Controls.Add(panel7);
            Controls.Add(panel6);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panelRecepcion);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "FormMain";
            Text = " Pokemon Center";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelRecepcion.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private Panel panelRecepcion;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private Panel panel9;
        private Panel panel10;
        private Panel panel11;
        private Panel panel12;
        private Panel panel13;
        private Panel panel14;
        private Panel panel15;
        private ToolStripMenuItem iniciarToolStripMenuItem;
        private ToolStripMenuItem pausarToolStripMenuItem;
        private ToolStripMenuItem reiniciarToolStripMenuItem;
        private ToolStripMenuItem pacientesToolStripMenuItem;
        private ToolStripMenuItem agregarPacienteToolStripMenuItem;
        private ToolStripMenuItem cargarArchivoToolStripMenuItem;
        private ToolStripMenuItem acercaDeToolStripMenuItem;
        private ListBox listaEspera;
    }
}
