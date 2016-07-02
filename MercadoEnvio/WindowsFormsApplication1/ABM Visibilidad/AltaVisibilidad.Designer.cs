namespace WindowsFormsApplication1.ABM_Visibilidad
{
    partial class AltaVisibilidad
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
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.txt_comTipo = new System.Windows.Forms.TextBox();
            this.txt_comProd = new System.Windows.Forms.TextBox();
            this.txt_comEnvio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cb_habilitado = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txt_nombre
            // 
            this.txt_nombre.Location = new System.Drawing.Point(143, 29);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(114, 20);
            this.txt_nombre.TabIndex = 0;
            // 
            // txt_comTipo
            // 
            this.txt_comTipo.Location = new System.Drawing.Point(143, 72);
            this.txt_comTipo.Name = "txt_comTipo";
            this.txt_comTipo.Size = new System.Drawing.Size(74, 20);
            this.txt_comTipo.TabIndex = 1;
            // 
            // txt_comProd
            // 
            this.txt_comProd.Location = new System.Drawing.Point(143, 118);
            this.txt_comProd.Name = "txt_comProd";
            this.txt_comProd.Size = new System.Drawing.Size(74, 20);
            this.txt_comProd.TabIndex = 2;
            // 
            // txt_comEnvio
            // 
            this.txt_comEnvio.Location = new System.Drawing.Point(143, 163);
            this.txt_comEnvio.Name = "txt_comEnvio";
            this.txt_comEnvio.Size = new System.Drawing.Size(74, 20);
            this.txt_comEnvio.TabIndex = 3;
            this.txt_comEnvio.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nombre";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comision por tipo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Comision por prod. vendido";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Comision por envio";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(71, 250);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cb_habilitado
            // 
            this.cb_habilitado.AutoSize = true;
            this.cb_habilitado.Location = new System.Drawing.Point(30, 211);
            this.cb_habilitado.Name = "cb_habilitado";
            this.cb_habilitado.Size = new System.Drawing.Size(73, 17);
            this.cb_habilitado.TabIndex = 9;
            this.cb_habilitado.Text = "Habilitado";
            this.cb_habilitado.UseVisualStyleBackColor = true;
            // 
            // AltaVisibilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 291);
            this.Controls.Add(this.cb_habilitado);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_comEnvio);
            this.Controls.Add(this.txt_comProd);
            this.Controls.Add(this.txt_comTipo);
            this.Controls.Add(this.txt_nombre);
            this.Name = "AltaVisibilidad";
            this.Text = "AltaVisibilidad";
            this.Load += new System.EventHandler(this.AltaVisibilidad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.TextBox txt_comTipo;
        private System.Windows.Forms.TextBox txt_comProd;
        private System.Windows.Forms.TextBox txt_comEnvio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cb_habilitado;
    }
}