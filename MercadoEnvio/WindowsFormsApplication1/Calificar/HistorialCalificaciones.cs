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

namespace WindowsFormsApplication1.Calificar
{
    public partial class HistorialCalificaciones : Form
    {
        private int idUserActual;

        public HistorialCalificaciones(int idUserActual)
        {
            InitializeComponent();
            this.idUserActual = idUserActual;
        }

        private void HistorialCalificaciones_Load(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerUltimasCalificaciones = bd.obtenerStoredProcedure("obtenerUltimasCalificaciones");
            spObtenerUltimasCalificaciones.Parameters.Add("@Id_User", SqlDbType.Int).Value = idUserActual;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerUltimasCalificaciones;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Desc_Public"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Cant_Estrellas"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Desc_Calif"].ToString();

            }

            var spObtenerCalificaciones = bd.obtenerStoredProcedure("obtenerCalificaciones");
            spObtenerCalificaciones.Parameters.Add("@Id_User", SqlDbType.Int).Value = idUserActual;

            SqlDataAdapter sda2 = new SqlDataAdapter();
            sda2.SelectCommand = spObtenerCalificaciones;
            DataTable dbdataset2 = new DataTable();
            sda2.Fill(dbdataset2);
            dataGridView2.Rows.Clear();
            foreach (DataRow item in dbdataset2.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = item["Cant_Estrellas"].ToString();
                dataGridView2.Rows[n].Cells[1].Value = item["cant_calif"].ToString();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
