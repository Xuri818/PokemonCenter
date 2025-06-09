namespace PokemonCenter.UI
{
    partial class FormAgregarPaciente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelNombre = new Label();
            labelPadecimiento = new Label();
            buttonAgregar = new Button();
            comboBoxNombre = new ComboBox();
            comboBoxPadecimiento = new ComboBox();
            SuspendLayout();
            // 
            // labelNombre
            // 
            labelNombre.AutoSize = true;
            labelNombre.Location = new Point(51, 105);
            labelNombre.Name = "labelNombre";
            labelNombre.Size = new Size(51, 15);
            labelNombre.TabIndex = 0;
            labelNombre.Text = "Nombre";
            labelNombre.Click += label1_Click;
            // 
            // labelPadecimiento
            // 
            labelPadecimiento.AutoSize = true;
            labelPadecimiento.Location = new Point(22, 157);
            labelPadecimiento.Name = "labelPadecimiento";
            labelPadecimiento.Size = new Size(80, 15);
            labelPadecimiento.TabIndex = 1;
            labelPadecimiento.Text = "Padecimiento";
            // 
            // buttonAgregar
            // 
            buttonAgregar.Location = new Point(99, 256);
            buttonAgregar.Name = "buttonAgregar";
            buttonAgregar.Size = new Size(75, 23);
            buttonAgregar.TabIndex = 2;
            buttonAgregar.Text = "Agregar";
            buttonAgregar.UseVisualStyleBackColor = true;
            buttonAgregar.Click += button1_Click;
            // 
            // comboBoxNombre
            // 
            comboBoxNombre.FormattingEnabled = true;
            comboBoxNombre.Location = new Point(119, 97);
            comboBoxNombre.Name = "comboBoxNombre";
            comboBoxNombre.Size = new Size(121, 23);
            comboBoxNombre.TabIndex = 3;
            // 
            // comboBoxPadecimiento
            // 
            comboBoxPadecimiento.FormattingEnabled = true;
            comboBoxPadecimiento.Location = new Point(119, 149);
            comboBoxPadecimiento.Name = "comboBoxPadecimiento";
            comboBoxPadecimiento.Size = new Size(121, 23);
            comboBoxPadecimiento.TabIndex = 4;
            // 
            // FormAgregarPaciente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(266, 314);
            Controls.Add(comboBoxPadecimiento);
            Controls.Add(comboBoxNombre);
            Controls.Add(buttonAgregar);
            Controls.Add(labelPadecimiento);
            Controls.Add(labelNombre);
            Name = "FormAgregarPaciente";
            Text = "FormAgregarPaciente";
            Load += FormAgregarPaciente_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelNombre;
        private Label labelPadecimiento;
        private Button buttonAgregar;
        private ComboBox comboBoxNombre;
        private ComboBox comboBoxPadecimiento;
    }
}