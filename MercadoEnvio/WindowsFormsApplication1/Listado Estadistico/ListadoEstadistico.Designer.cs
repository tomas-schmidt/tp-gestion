namespace WindowsFormsApplication1.Listado_Estadistico
{
    partial class ListadoEstadistico
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_visibilidad = new System.Windows.Forms.Label();
            this.cb_visibilidad = new System.Windows.Forms.ComboBox();
            this.cb_rubros = new System.Windows.Forms.ComboBox();
            this.lbl_rubro = new System.Windows.Forms.Label();
            this.btn_mayorMontoFacturas = new System.Windows.Forms.Button();
            this.btn_mayorCantFacturas = new System.Windows.Forms.Button();
            this.btn_mayorProdsComprados = new System.Windows.Forms.Button();
            this.btn_prodsNoVendidos = new System.Windows.Forms.Button();
            this.cb_tipoListado = new System.Windows.Forms.ComboBox();
            this.cb_trimestre = new System.Windows.Forms.ComboBox();
            this.txt_anio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_visibilidad);
            this.groupBox1.Controls.Add(this.cb_visibilidad);
            this.groupBox1.Controls.Add(this.cb_rubros);
            this.groupBox1.Controls.Add(this.lbl_rubro);
            this.groupBox1.Controls.Add(this.btn_mayorMontoFacturas);
            this.groupBox1.Controls.Add(this.btn_mayorCantFacturas);
            this.groupBox1.Controls.Add(this.btn_mayorProdsComprados);
            this.groupBox1.Controls.Add(this.btn_prodsNoVendidos);
            this.groupBox1.Controls.Add(this.cb_tipoListado);
            this.groupBox1.Controls.Add(this.cb_trimestre);
            this.groupBox1.Controls.Add(this.txt_anio);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(605, 138);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del listado";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lbl_visibilidad
            // 
            this.lbl_visibilidad.AutoSize = true;
            this.lbl_visibilidad.Location = new System.Drawing.Point(267, 66);
            this.lbl_visibilidad.Name = "lbl_visibilidad";
            this.lbl_visibilidad.Size = new System.Drawing.Size(81, 13);
            this.lbl_visibilidad.TabIndex = 13;
            this.lbl_visibilidad.Text = "Elegir visibilidad";
            // 
            // cb_visibilidad
            // 
            this.cb_visibilidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_visibilidad.FormattingEnabled = true;
            this.cb_visibilidad.Location = new System.Drawing.Point(354, 58);
            this.cb_visibilidad.Name = "cb_visibilidad";
            this.cb_visibilidad.Size = new System.Drawing.Size(169, 21);
            this.cb_visibilidad.TabIndex = 12;
            // 
            // cb_rubros
            // 
            this.cb_rubros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_rubros.FormattingEnabled = true;
            this.cb_rubros.Location = new System.Drawing.Point(354, 58);
            this.cb_rubros.Name = "cb_rubros";
            this.cb_rubros.Size = new System.Drawing.Size(169, 21);
            this.cb_rubros.TabIndex = 11;
            // 
            // lbl_rubro
            // 
            this.lbl_rubro.AutoSize = true;
            this.lbl_rubro.Location = new System.Drawing.Point(280, 64);
            this.lbl_rubro.Name = "lbl_rubro";
            this.lbl_rubro.Size = new System.Drawing.Size(60, 13);
            this.lbl_rubro.TabIndex = 10;
            this.lbl_rubro.Text = "Elegir rubro";
            // 
            // btn_mayorMontoFacturas
            // 
            this.btn_mayorMontoFacturas.Location = new System.Drawing.Point(453, 98);
            this.btn_mayorMontoFacturas.Name = "btn_mayorMontoFacturas";
            this.btn_mayorMontoFacturas.Size = new System.Drawing.Size(110, 23);
            this.btn_mayorMontoFacturas.TabIndex = 9;
            this.btn_mayorMontoFacturas.Text = "Consultar";
            this.btn_mayorMontoFacturas.UseVisualStyleBackColor = true;
            this.btn_mayorMontoFacturas.Click += new System.EventHandler(this.btn_mayorMontoFacturas_Click);
            // 
            // btn_mayorCantFacturas
            // 
            this.btn_mayorCantFacturas.Location = new System.Drawing.Point(453, 99);
            this.btn_mayorCantFacturas.Name = "btn_mayorCantFacturas";
            this.btn_mayorCantFacturas.Size = new System.Drawing.Size(110, 23);
            this.btn_mayorCantFacturas.TabIndex = 8;
            this.btn_mayorCantFacturas.Text = "Consultar";
            this.btn_mayorCantFacturas.UseVisualStyleBackColor = true;
            this.btn_mayorCantFacturas.Click += new System.EventHandler(this.btn_mayorCantFacturas_Click);
            // 
            // btn_mayorProdsComprados
            // 
            this.btn_mayorProdsComprados.Location = new System.Drawing.Point(453, 99);
            this.btn_mayorProdsComprados.Name = "btn_mayorProdsComprados";
            this.btn_mayorProdsComprados.Size = new System.Drawing.Size(110, 23);
            this.btn_mayorProdsComprados.TabIndex = 7;
            this.btn_mayorProdsComprados.Text = "Consultar";
            this.btn_mayorProdsComprados.UseVisualStyleBackColor = true;
            this.btn_mayorProdsComprados.Click += new System.EventHandler(this.btn_mayorProdsComprados_Click);
            // 
            // btn_prodsNoVendidos
            // 
            this.btn_prodsNoVendidos.Location = new System.Drawing.Point(453, 99);
            this.btn_prodsNoVendidos.Name = "btn_prodsNoVendidos";
            this.btn_prodsNoVendidos.Size = new System.Drawing.Size(110, 23);
            this.btn_prodsNoVendidos.TabIndex = 6;
            this.btn_prodsNoVendidos.Text = "Consultar";
            this.btn_prodsNoVendidos.UseVisualStyleBackColor = true;
            this.btn_prodsNoVendidos.Click += new System.EventHandler(this.btn_prodsNoVendidos_Click);
            // 
            // cb_tipoListado
            // 
            this.cb_tipoListado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_tipoListado.FormattingEnabled = true;
            this.cb_tipoListado.Location = new System.Drawing.Point(349, 23);
            this.cb_tipoListado.Name = "cb_tipoListado";
            this.cb_tipoListado.Size = new System.Drawing.Size(214, 21);
            this.cb_tipoListado.TabIndex = 5;
            this.cb_tipoListado.SelectedIndexChanged += new System.EventHandler(this.cb_tipoListado_SelectedIndexChanged);
            // 
            // cb_trimestre
            // 
            this.cb_trimestre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_trimestre.FormattingEnabled = true;
            this.cb_trimestre.Location = new System.Drawing.Point(95, 63);
            this.cb_trimestre.Name = "cb_trimestre";
            this.cb_trimestre.Size = new System.Drawing.Size(88, 21);
            this.cb_trimestre.TabIndex = 4;
            // 
            // txt_anio
            // 
            this.txt_anio.Location = new System.Drawing.Point(95, 24);
            this.txt_anio.Name = "txt_anio";
            this.txt_anio.Size = new System.Drawing.Size(109, 20);
            this.txt_anio.TabIndex = 3;
            this.txt_anio.TextChanged += new System.EventHandler(this.txt_anio_TextChanged);
            this.txt_anio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_anio_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tipo de listado";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Trimestre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Año";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView4);
            this.groupBox2.Controls.Add(this.dataGridView3);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(13, 177);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(604, 212);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Listado Estadistico";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9});
            this.dataGridView4.Location = new System.Drawing.Point(12, 22);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.Size = new System.Drawing.Size(578, 176);
            this.dataGridView4.TabIndex = 3;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column8.HeaderText = "Username";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Monto total";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column7});
            this.dataGridView3.Location = new System.Drawing.Point(12, 22);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.Size = new System.Drawing.Size(578, 176);
            this.dataGridView3.TabIndex = 2;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "Username";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Cantidad de facturas";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column5,
            this.Column11});
            this.dataGridView2.Location = new System.Drawing.Point(12, 22);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(578, 176);
            this.dataGridView2.TabIndex = 1;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "Username";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Cantidad de productos";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Rubro";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column10,
            this.Column12});
            this.dataGridView1.Location = new System.Drawing.Point(12, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(578, 176);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Username";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Publicacion";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Stock";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Visibilidad";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Fecha final";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // ListadoEstadistico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 401);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ListadoEstadistico";
            this.Text = "Listado estadistico";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_tipoListado;
        private System.Windows.Forms.ComboBox cb_trimestre;
        private System.Windows.Forms.TextBox txt_anio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_mayorMontoFacturas;
        private System.Windows.Forms.Button btn_mayorCantFacturas;
        private System.Windows.Forms.Button btn_mayorProdsComprados;
        private System.Windows.Forms.Button btn_prodsNoVendidos;
        private System.Windows.Forms.ComboBox cb_rubros;
        private System.Windows.Forms.Label lbl_rubro;
        private System.Windows.Forms.ComboBox cb_visibilidad;
        private System.Windows.Forms.Label lbl_visibilidad;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;

    }
}