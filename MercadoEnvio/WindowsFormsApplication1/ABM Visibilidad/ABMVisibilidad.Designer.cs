﻿namespace WindowsFormsApplication1.ABM_Visibilidad
{
    partial class ABMVisibilidad
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
            this.btn_altaVisibilidad = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_altaVisibilidad
            // 
            this.btn_altaVisibilidad.Location = new System.Drawing.Point(79, 43);
            this.btn_altaVisibilidad.Name = "btn_altaVisibilidad";
            this.btn_altaVisibilidad.Size = new System.Drawing.Size(119, 23);
            this.btn_altaVisibilidad.TabIndex = 0;
            this.btn_altaVisibilidad.Text = "Alta visibilidad";
            this.btn_altaVisibilidad.UseVisualStyleBackColor = true;
            this.btn_altaVisibilidad.Click += new System.EventHandler(this.btn_altaVisibilidad_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(55, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Modificar/Baja de visibilidad";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ABMVisibilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 187);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_altaVisibilidad);
            this.Name = "ABMVisibilidad";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_altaVisibilidad;
        private System.Windows.Forms.Button button1;
    }
}