namespace WindowsFormsApplication1.ABM_Usuario
{
    partial class CambiarContrasenia
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_userName = new System.Windows.Forms.TextBox();
            this.txt_contrasenia1 = new System.Windows.Forms.TextBox();
            this.txt_contrasenia2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nueva contraseña";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Repetir contraseña";
            // 
            // txt_userName
            // 
            this.txt_userName.Location = new System.Drawing.Point(74, 28);
            this.txt_userName.Name = "txt_userName";
            this.txt_userName.ReadOnly = true;
            this.txt_userName.Size = new System.Drawing.Size(192, 20);
            this.txt_userName.TabIndex = 3;
            // 
            // txt_contrasenia1
            // 
            this.txt_contrasenia1.Location = new System.Drawing.Point(116, 77);
            this.txt_contrasenia1.Name = "txt_contrasenia1";
            this.txt_contrasenia1.PasswordChar = '*';
            this.txt_contrasenia1.Size = new System.Drawing.Size(192, 20);
            this.txt_contrasenia1.TabIndex = 4;
            // 
            // txt_contrasenia2
            // 
            this.txt_contrasenia2.Location = new System.Drawing.Point(116, 122);
            this.txt_contrasenia2.Name = "txt_contrasenia2";
            this.txt_contrasenia2.PasswordChar = '*';
            this.txt_contrasenia2.Size = new System.Drawing.Size(192, 20);
            this.txt_contrasenia2.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(74, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Cambiar contraseña";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CambiarContrasenia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 230);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_contrasenia2);
            this.Controls.Add(this.txt_contrasenia1);
            this.Controls.Add(this.txt_userName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CambiarContrasenia";
            this.Text = "CambiarContrasenia";
            this.Load += new System.EventHandler(this.CambiarContrasenia_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_userName;
        private System.Windows.Forms.TextBox txt_contrasenia1;
        private System.Windows.Forms.TextBox txt_contrasenia2;
        private System.Windows.Forms.Button button1;
    }
}