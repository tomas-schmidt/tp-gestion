namespace WindowsFormsApplication1
{
    partial class ElegirFuncionalidad
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
            this.dataGridView_funcionalidades = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Funcionalidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_funcionalidades)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_funcionalidades
            // 
            this.dataGridView_funcionalidades.AllowUserToAddRows = false;
            this.dataGridView_funcionalidades.AllowUserToDeleteRows = false;
            this.dataGridView_funcionalidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_funcionalidades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Funcionalidad,
            this.Column1});
            this.dataGridView_funcionalidades.Location = new System.Drawing.Point(18, 43);
            this.dataGridView_funcionalidades.Name = "dataGridView_funcionalidades";
            this.dataGridView_funcionalidades.ReadOnly = true;
            this.dataGridView_funcionalidades.Size = new System.Drawing.Size(276, 172);
            this.dataGridView_funcionalidades.TabIndex = 0;
            this.dataGridView_funcionalidades.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_funcionalidades_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Elegir funcionalidad";
            // 
            // Funcionalidad
            // 
            this.Funcionalidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Funcionalidad.HeaderText = "Funcionalidad";
            this.Funcionalidad.Name = "Funcionalidad";
            this.Funcionalidad.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Seleccionar";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // ElegirFuncionalidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 235);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_funcionalidades);
            this.Name = "ElegirFuncionalidad";
            this.Text = "ElegirFuncionalidad";
            this.Load += new System.EventHandler(this.ElegirFuncionalidad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_funcionalidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_funcionalidades;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Funcionalidad;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
    }
}