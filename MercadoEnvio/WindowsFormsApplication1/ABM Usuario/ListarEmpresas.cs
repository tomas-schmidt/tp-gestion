using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.ConexionBD;

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class ListarEmpresas : Form
    {
        public ListarEmpresas()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ListarEmpresas_Load(object sender, EventArgs e)
        {

        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            string consulta;
            consulta = "select * from C_HASHTAG.Empresa where Id_Empresa is not null";

            if (txt_Cuit.Text != "")
            {
                consulta = consulta + " and Cuit = '" + txt_Cuit.Text + "'";
            } 
            if (txt_razonSocial.Text != "")
            {
                consulta = consulta + " and Razon_Social like '%" + txt_razonSocial.Text +"%'";
            }
            if (txt_Email.Text != "")
            {
                consulta = consulta + " and Mail like '%" + txt_Email.Text + "%'";
            }

            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerEmpresas = bd.obtenerConsulta(consulta);

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerEmpresas;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Id_Empresa"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Razon_Social"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Cuit"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Mail"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = "Dar de baja";
                dataGridView1.Rows[n].Cells[5].Value = "Modificar";

            }
        }
    }
}
