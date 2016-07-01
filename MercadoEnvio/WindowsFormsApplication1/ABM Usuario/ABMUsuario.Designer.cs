namespace WindowsFormsApplication1.ABM_Usuario
{
    partial class ABMUsuario
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
            this.btn_crearEmpresa = new System.Windows.Forms.Button();
            this.btn_crearCliente = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_crearEmpresa
            // 
            this.btn_crearEmpresa.Location = new System.Drawing.Point(53, 55);
            this.btn_crearEmpresa.Name = "btn_crearEmpresa";
            this.btn_crearEmpresa.Size = new System.Drawing.Size(160, 24);
            this.btn_crearEmpresa.TabIndex = 0;
            this.btn_crearEmpresa.Text = "Crear empresa";
            this.btn_crearEmpresa.UseVisualStyleBackColor = true;
            this.btn_crearEmpresa.Click += new System.EventHandler(this.btn_crearEmpresa_Click);
            // 
            // btn_crearCliente
            // 
            this.btn_crearCliente.Location = new System.Drawing.Point(50, 155);
            this.btn_crearCliente.Name = "btn_crearCliente";
            this.btn_crearCliente.Size = new System.Drawing.Size(162, 24);
            this.btn_crearCliente.TabIndex = 1;
            this.btn_crearCliente.Text = "Crear cliente";
            this.btn_crearCliente.UseVisualStyleBackColor = true;
            this.btn_crearCliente.Click += new System.EventHandler(this.btn_crearCliente_Click);
            // 
            // ABMUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btn_crearCliente);
            this.Controls.Add(this.btn_crearEmpresa);
            this.Name = "ABMUsuario";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_crearEmpresa;
        private System.Windows.Forms.Button btn_crearCliente;
    }
}