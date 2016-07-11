namespace WindowsFormsApplication1.Historial_Cliente
{
    partial class HistorialCliente
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
            this.btn_compras = new System.Windows.Forms.Button();
            this.btn_ofertas = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_compras
            // 
            this.btn_compras.Location = new System.Drawing.Point(52, 50);
            this.btn_compras.Name = "btn_compras";
            this.btn_compras.Size = new System.Drawing.Size(181, 23);
            this.btn_compras.TabIndex = 0;
            this.btn_compras.Text = "Compras concretadas";
            this.btn_compras.UseVisualStyleBackColor = true;
            this.btn_compras.Click += new System.EventHandler(this.btn_compras_Click);
            // 
            // btn_ofertas
            // 
            this.btn_ofertas.Location = new System.Drawing.Point(52, 120);
            this.btn_ofertas.Name = "btn_ofertas";
            this.btn_ofertas.Size = new System.Drawing.Size(181, 23);
            this.btn_ofertas.TabIndex = 1;
            this.btn_ofertas.Text = "Ofertas realizadas";
            this.btn_ofertas.UseVisualStyleBackColor = true;
            this.btn_ofertas.Click += new System.EventHandler(this.btn_ofertas_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(52, 192);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(181, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Historial calificaciones";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // HistorialCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_ofertas);
            this.Controls.Add(this.btn_compras);
            this.Name = "HistorialCliente";
            this.Text = "Historial Cliente";
            this.Load += new System.EventHandler(this.HistorialCliente_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_compras;
        private System.Windows.Forms.Button btn_ofertas;
        private System.Windows.Forms.Button button3;
    }
}