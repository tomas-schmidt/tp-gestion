namespace WindowsFormsApplication1.Calificar
{
    partial class Calificar
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
            this.cb_cantEstrellas = new System.Windows.Forms.ComboBox();
            this.txt_comentario = new System.Windows.Forms.TextBox();
            this.btn_calificar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cb_cantEstrellas
            // 
            this.cb_cantEstrellas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cantEstrellas.FormattingEnabled = true;
            this.cb_cantEstrellas.Location = new System.Drawing.Point(98, 28);
            this.cb_cantEstrellas.Name = "cb_cantEstrellas";
            this.cb_cantEstrellas.Size = new System.Drawing.Size(121, 21);
            this.cb_cantEstrellas.TabIndex = 0;
            // 
            // txt_comentario
            // 
            this.txt_comentario.Location = new System.Drawing.Point(39, 108);
            this.txt_comentario.Multiline = true;
            this.txt_comentario.Name = "txt_comentario";
            this.txt_comentario.Size = new System.Drawing.Size(217, 47);
            this.txt_comentario.TabIndex = 1;
            // 
            // btn_calificar
            // 
            this.btn_calificar.Location = new System.Drawing.Point(75, 187);
            this.btn_calificar.Name = "btn_calificar";
            this.btn_calificar.Size = new System.Drawing.Size(144, 23);
            this.btn_calificar.TabIndex = 2;
            this.btn_calificar.Text = "Calificar";
            this.btn_calificar.UseVisualStyleBackColor = true;
            this.btn_calificar.Click += new System.EventHandler(this.btn_calificar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Estrellas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Comentario (Opcional)";
            // 
            // Calificar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 235);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_calificar);
            this.Controls.Add(this.txt_comentario);
            this.Controls.Add(this.cb_cantEstrellas);
            this.Name = "Calificar";
            this.Text = "Calificar";
            this.Load += new System.EventHandler(this.Calificar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_cantEstrellas;
        private System.Windows.Forms.TextBox txt_comentario;
        private System.Windows.Forms.Button btn_calificar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}