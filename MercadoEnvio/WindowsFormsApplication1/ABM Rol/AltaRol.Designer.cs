namespace WindowsFormsApplication1.ABM_Rol
{
    partial class AltaRol
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
            this.txt_nombreRol = new System.Windows.Forms.TextBox();
            this.cb_habilitado = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_guardar = new System.Windows.Forms.Button();
            this.dataGridView_funcionalidades = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_funcionalidades)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_nombreRol
            // 
            this.txt_nombreRol.Location = new System.Drawing.Point(98, 12);
            this.txt_nombreRol.Name = "txt_nombreRol";
            this.txt_nombreRol.Size = new System.Drawing.Size(100, 20);
            this.txt_nombreRol.TabIndex = 0;
            this.txt_nombreRol.TextChanged += new System.EventHandler(this.txt_nombreRol_TextChanged);
            // 
            // cb_habilitado
            // 
            this.cb_habilitado.AutoSize = true;
            this.cb_habilitado.Location = new System.Drawing.Point(37, 47);
            this.cb_habilitado.Name = "cb_habilitado";
            this.cb_habilitado.Size = new System.Drawing.Size(73, 17);
            this.cb_habilitado.TabIndex = 1;
            this.cb_habilitado.Text = "Habilitado";
            this.cb_habilitado.UseVisualStyleBackColor = true;
            this.cb_habilitado.CheckedChanged += new System.EventHandler(this.cb_habilitado_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nombre";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_guardar
            // 
            this.btn_guardar.Location = new System.Drawing.Point(162, 268);
            this.btn_guardar.Name = "btn_guardar";
            this.btn_guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_guardar.TabIndex = 4;
            this.btn_guardar.Text = "Guardar";
            this.btn_guardar.UseVisualStyleBackColor = true;
            this.btn_guardar.Click += new System.EventHandler(this.btn_guardar_Click);
            // 
            // dataGridView_funcionalidades
            // 
            this.dataGridView_funcionalidades.AllowUserToAddRows = false;
            this.dataGridView_funcionalidades.AllowUserToDeleteRows = false;
            this.dataGridView_funcionalidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_funcionalidades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView_funcionalidades.Location = new System.Drawing.Point(22, 93);
            this.dataGridView_funcionalidades.Name = "dataGridView_funcionalidades";
            this.dataGridView_funcionalidades.Size = new System.Drawing.Size(383, 150);
            this.dataGridView_funcionalidades.TabIndex = 5;
            this.dataGridView_funcionalidades.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Seleccionar";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Id_Funcionalidad";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Nombre_Funcionalidad";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // AltaRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 318);
            this.Controls.Add(this.dataGridView_funcionalidades);
            this.Controls.Add(this.btn_guardar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_habilitado);
            this.Controls.Add(this.txt_nombreRol);
            this.Name = "AltaRol";
            this.Text = "AltaRol";
            this.Load += new System.EventHandler(this.AltaRol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_funcionalidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_nombreRol;
        private System.Windows.Forms.CheckBox cb_habilitado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_guardar;
        private System.Windows.Forms.DataGridView dataGridView_funcionalidades;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}