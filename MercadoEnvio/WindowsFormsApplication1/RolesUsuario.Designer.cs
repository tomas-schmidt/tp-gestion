namespace WindowsFormsApplication1
{
    partial class RolesUsuario
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
            this.dataGridView_roles = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_roles)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_roles
            // 
            this.dataGridView_roles.AllowUserToAddRows = false;
            this.dataGridView_roles.AllowUserToDeleteRows = false;

            this.dataGridView_roles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_roles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView_roles.Location = new System.Drawing.Point(18, 35);
            this.dataGridView_roles.Name = "dataGridView_roles";
            this.dataGridView_roles.Size = new System.Drawing.Size(246, 197);
            this.dataGridView_roles.TabIndex = 0;
            this.dataGridView_roles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_roles_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Acceder como";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            //
            // Column2
            //
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Nombre";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Elegir Rol";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // RolesUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 256);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_roles);
            this.Name = "RolesUsuario";
            this.Text = "Roles de usuario";
            this.Load += new System.EventHandler(this.FuncionalidadesUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_roles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_roles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}