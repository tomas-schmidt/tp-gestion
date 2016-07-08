namespace WindowsFormsApplication1.Generar_Publicación
{
    partial class GenerarSubasta
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
            this.btn_generarSubasta = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txt_descripcion = new System.Windows.Forms.TextBox();
            this.txt_montoInicial = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_generarSubasta
            // 
            this.btn_generarSubasta.Location = new System.Drawing.Point(116, 473);
            this.btn_generarSubasta.Name = "btn_generarSubasta";
            this.btn_generarSubasta.Size = new System.Drawing.Size(293, 23);
            this.btn_generarSubasta.TabIndex = 20;
            this.btn_generarSubasta.Text = "Generar publicacion de subasta";
            this.btn_generarSubasta.UseVisualStyleBackColor = true;
            this.btn_generarSubasta.Click += new System.EventHandler(this.btn_generarSubasta_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(114, 73);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 19;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(29, 403);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(114, 17);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "Habilitar preguntas";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txt_descripcion
            // 
            this.txt_descripcion.Location = new System.Drawing.Point(95, 334);
            this.txt_descripcion.Multiline = true;
            this.txt_descripcion.Name = "txt_descripcion";
            this.txt_descripcion.Size = new System.Drawing.Size(395, 44);
            this.txt_descripcion.TabIndex = 17;
            // 
            // txt_montoInicial
            // 
            this.txt_montoInicial.Location = new System.Drawing.Point(114, 24);
            this.txt_montoInicial.Name = "txt_montoInicial";
            this.txt_montoInicial.Size = new System.Drawing.Size(100, 20);
            this.txt_montoInicial.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Descripcion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Elegir visibilidad";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Monto inicial";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(114, 121);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Elegir estado";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Elegir rubros";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(114, 159);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(349, 150);
            this.dataGridView1.TabIndex = 23;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Seleccionar";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Rubros";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(29, 440);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(93, 17);
            this.checkBox2.TabIndex = 25;
            this.checkBox2.Text = "Habilitar envio";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // GenerarSubasta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 510);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_generarSubasta);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txt_descripcion);
            this.Controls.Add(this.txt_montoInicial);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "GenerarSubasta";
            this.Text = "GenerarSubasta";
            this.Load += new System.EventHandler(this.GenerarSubasta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_generarSubasta;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txt_descripcion;
        private System.Windows.Forms.TextBox txt_montoInicial;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}