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

namespace WindowsFormsApplication1.ABM_Visibilidad
{
    public partial class ListarVisibilidades : Form
    {
        public ListarVisibilidades()
        {
            InitializeComponent();
        }

        private void ListarVisibilidadescs_Load(object sender, EventArgs e)
        {
            this.cargarTabla();
        }

        private void cargarTabla()
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerVisibilidades = bd.obtenerStoredProcedure("obtenerVisibilidades");

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerVisibilidades;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Id_Visibilidad"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Visibilidad_Desc"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Habilitado"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = "Baja/Alta";
                dataGridView1.Rows[n].Cells[4].Value = "Modificar";

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && (e.RowIndex != -1))
            {
                BaseDeDatos bd = new BaseDeDatos();
                var spCambiarEstadoVisibilidad = bd.obtenerStoredProcedure("cambiarEstadoVisibilidad");
                spCambiarEstadoVisibilidad.Parameters.Add("@Id_Visibilidad", SqlDbType.VarChar).Value = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                spCambiarEstadoVisibilidad.ExecuteNonQuery();
                spCambiarEstadoVisibilidad.Connection.Close();
                MessageBox.Show("La visibilidad fue dada de baja/alta");
                this.cargarTabla();
            }
            if (e.ColumnIndex == 4 && (e.RowIndex != -1))
            {
                ModificarVisibilidad mv = new ModificarVisibilidad(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                mv.Show();

            }
        }


    }
}
