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

            double rows = ((dataGridView1.Rows.Count));
            double paginas = Math.Ceiling(rows / 10);
            if (rows > 0)
            {
                for (int j = 0; j < rows; j++)
                {
                    dataGridView1.Rows[j].Visible = false;
                }
            }

            if (paginas > 0)
            {
                for (int i = 1; i <= paginas; i++)
                {
                    cb_pags.Items.Add(i);
                }
                cb_pags.SelectedItem = cb_pags.Items[0];
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cb_pags_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rows = ((dataGridView1.Rows.Count));

            if (rows > 0)
            {
                for (int j = 0; j < rows; j++)
                {
                    dataGridView1.Rows[j].Visible = false;
                }
            }

            int pag = (((Convert.ToInt32(cb_pags.SelectedIndex)) + 1) * 10);
            for (int i = pag - 10; i < pag; i++)
            {
                if (dataGridView1.Rows.Count > i)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }
        }
    }
}
