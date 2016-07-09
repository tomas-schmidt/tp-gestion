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
    public partial class ListarComprasSinCalificar : Form
    {
        private int idUserActual;

        public ListarComprasSinCalificar(int idUserActual)
        {
            this.idUserActual = idUserActual;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerComprasSinCalificar = bd.obtenerStoredProcedure("obtenerComprasSinCalificar");
            spObtenerComprasSinCalificar.Parameters.Add("@Id_User", SqlDbType.Int).Value = idUserActual;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerComprasSinCalificar;
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
                dataGridView1.Rows[n].Cells[4].Value = "Calificar";
                dataGridView1.Rows[n].Cells[5].Value = item["Id_Compra"].ToString();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                int idCompra = (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()));
                Calificar c = new Calificar(idCompra);
                c.Show();
            }
        }
    }
}
