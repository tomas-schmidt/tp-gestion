﻿namespace WindowsFormsApplication1.Generar_Publicación
{
    partial class GenerarPublicacion
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
            this.btn_generarCompra = new System.Windows.Forms.Button();
            this.btn_generarSubasta = new System.Windows.Forms.Button();
            this.btn_borradores = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_generarCompra
            // 
            this.btn_generarCompra.Location = new System.Drawing.Point(52, 31);
            this.btn_generarCompra.Name = "btn_generarCompra";
            this.btn_generarCompra.Size = new System.Drawing.Size(172, 35);
            this.btn_generarCompra.TabIndex = 0;
            this.btn_generarCompra.Text = "Generar Compra inmediata";
            this.btn_generarCompra.UseVisualStyleBackColor = true;
            this.btn_generarCompra.Click += new System.EventHandler(this.btn_generarCompra_Click);
            // 
            // btn_generarSubasta
            // 
            this.btn_generarSubasta.Location = new System.Drawing.Point(52, 97);
            this.btn_generarSubasta.Name = "btn_generarSubasta";
            this.btn_generarSubasta.Size = new System.Drawing.Size(172, 35);
            this.btn_generarSubasta.TabIndex = 1;
            this.btn_generarSubasta.Text = "Generar Subasta";
            this.btn_generarSubasta.UseVisualStyleBackColor = true;
            this.btn_generarSubasta.Click += new System.EventHandler(this.btn_generarSubasta_Click);
            // 
            // btn_borradores
            // 
            this.btn_borradores.Location = new System.Drawing.Point(52, 163);
            this.btn_borradores.Name = "btn_borradores";
            this.btn_borradores.Size = new System.Drawing.Size(172, 35);
            this.btn_borradores.TabIndex = 2;
            this.btn_borradores.Text = "Modificar borradores";
            this.btn_borradores.UseVisualStyleBackColor = true;
            this.btn_borradores.Click += new System.EventHandler(this.btn_borradores_Click);
            // 
            // GenerarPublicacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 229);
            this.Controls.Add(this.btn_borradores);
            this.Controls.Add(this.btn_generarSubasta);
            this.Controls.Add(this.btn_generarCompra);
            this.Name = "GenerarPublicacion";
            this.Text = "Generar Publicacion";
            this.Load += new System.EventHandler(this.GenerarPublicacion_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_generarCompra;
        private System.Windows.Forms.Button btn_generarSubasta;
        private System.Windows.Forms.Button btn_borradores;
    }
}