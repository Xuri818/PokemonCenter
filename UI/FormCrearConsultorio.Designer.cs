namespace PokemonCenter.UI
{
    partial class FormCrearConsultorio
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
            chkDormido = new CheckBox();
            chkEnvenenado = new CheckBox();
            chkParalizado = new CheckBox();
            chkQuemado = new CheckBox();
            chkCongelado = new CheckBox();
            chkConfundido = new CheckBox();
            btnAceptar = new Button();
            SuspendLayout();
            // 
            // chkDormido
            // 
            chkDormido.AutoSize = true;
            chkDormido.Location = new Point(29, 74);
            chkDormido.Name = "chkDormido";
            chkDormido.Size = new Size(201, 19);
            chkDormido.TabIndex = 0;
            chkDormido.Text = "Transtornos del Sueño (Dormido)";
            chkDormido.UseVisualStyleBackColor = true;
            // 
            // chkEnvenenado
            // 
            chkEnvenenado.AutoSize = true;
            chkEnvenenado.Location = new Point(29, 99);
            chkEnvenenado.Name = "chkEnvenenado";
            chkEnvenenado.Size = new Size(162, 19);
            chkEnvenenado.TabIndex = 1;
            chkEnvenenado.Text = "Toxicología (Envenenado)";
            chkEnvenenado.UseVisualStyleBackColor = true;
            // 
            // chkParalizado
            // 
            chkParalizado.AutoSize = true;
            chkParalizado.Location = new Point(29, 124);
            chkParalizado.Name = "chkParalizado";
            chkParalizado.Size = new Size(165, 19);
            chkParalizado.TabIndex = 2;
            chkParalizado.Text = "Fisioterapeuta (Paralizado)";
            chkParalizado.UseVisualStyleBackColor = true;
            // 
            // chkQuemado
            // 
            chkQuemado.AutoSize = true;
            chkQuemado.Location = new Point(29, 149);
            chkQuemado.Name = "chkQuemado";
            chkQuemado.Size = new Size(215, 19);
            chkQuemado.TabIndex = 3;
            chkQuemado.Text = "Unidad de Quemaduras (Quemado)";
            chkQuemado.UseVisualStyleBackColor = true;
            // 
            // chkCongelado
            // 
            chkCongelado.AutoSize = true;
            chkCongelado.Location = new Point(29, 174);
            chkCongelado.Name = "chkCongelado";
            chkCongelado.Size = new Size(179, 19);
            chkCongelado.TabIndex = 4;
            chkCongelado.Text = "Descongelación (Congelado)";
            chkCongelado.UseVisualStyleBackColor = true;
            // 
            // chkConfundido
            // 
            chkConfundido.AutoSize = true;
            chkConfundido.Location = new Point(29, 199);
            chkConfundido.Name = "chkConfundido";
            chkConfundido.Size = new Size(220, 19);
            chkConfundido.TabIndex = 5;
            chkConfundido.Text = "Psicólogo (Confundido, Enamorado)";
            chkConfundido.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(91, 253);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(87, 28);
            btnAceptar.TabIndex = 6;
            btnAceptar.Text = "Aceptar";
            btnAceptar.UseVisualStyleBackColor = true;
            btnAceptar.Click += btnAceptar_Click;
            // 
            // FormCrearConsultorio
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(266, 314);
            Controls.Add(btnAceptar);
            Controls.Add(chkConfundido);
            Controls.Add(chkCongelado);
            Controls.Add(chkQuemado);
            Controls.Add(chkParalizado);
            Controls.Add(chkEnvenenado);
            Controls.Add(chkDormido);
            MaximizeBox = false;
            Name = "FormCrearConsultorio";
            Text = "Crear Consultorio";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkDormido;
        private CheckBox chkEnvenenado;
        private CheckBox chkParalizado;
        private CheckBox chkQuemado;
        private CheckBox chkCongelado;
        private CheckBox chkConfundido;
        private Button btnAceptar;
    }
}