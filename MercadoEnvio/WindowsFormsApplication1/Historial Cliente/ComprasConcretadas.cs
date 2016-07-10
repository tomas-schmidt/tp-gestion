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

namespace WindowsFormsApplication1.Historial_Cliente
{
    public partial class ComprasConcretadas : Form
    {
        private int idUserActual;

        public ComprasConcretadas(int idUserActual)
        {
            this.idUserActual = idUserActual;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerCompras = bd.obtenerStoredProcedure("obtenerCompras");
            spObtenerCompras.Parameters.Add("@Id_User", SqlDbType.Int).Value = idUserActual;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerCompras;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Descripcion"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Monto"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Cantidad"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Fecha"].ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
