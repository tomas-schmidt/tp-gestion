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
    public partial class ListarEmpresas : FormMaestro
    {
        public ListarEmpresas()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && (e.RowIndex != -1))
            {
                BaseDeDatos bd = new BaseDeDatos();
                var spcambiarEstadoEmpresa = bd.obtenerStoredProcedure("cambiarEstadoEmpresa");
                spcambiarEstadoEmpresa.Parameters.Add("@Id_Empresa", SqlDbType.VarChar).Value = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                spcambiarEstadoEmpresa.ExecuteNonQuery();
                spcambiarEstadoEmpresa.Connection.Close();
                MessageBox.Show("La empresa fue dada de baja/alta");
            }
            if (e.ColumnIndex == 6 && (e.RowIndex != -1))
            {
                ModificarEmpresa mr = new ModificarEmpresa(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                mr.Show();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ListarEmpresas_Load(object sender, EventArgs e)
        {
            
        }

        protected override void interactuar()
        {
            string consulta;
            consulta = "select e.*, Habilitado from C_HASHTAG.Empresa e JOIN C_HASHTAG.Usuario u ON (u.Id_User = e.Id_User) where Id_Empresa is not null";

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
                dataGridView1.Rows[n].Cells[4].Value = item["Habilitado"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = "Baja/Alta";
                dataGridView1.Rows[n].Cells[6].Value = "Modificar";

            }
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            this.submitir();
        }  

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            txt_Cuit.Clear();
            txt_Email.Clear();
            txt_razonSocial.Clear();
        }

        private void txt_Email_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_razonSocial_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Cuit_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
