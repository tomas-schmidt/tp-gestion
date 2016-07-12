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

namespace WindowsFormsApplication1.Generar_Publicación
{
    public partial class ListarBorradores : Form
    {
        private int idUserActual;

        public ListarBorradores(int idUserActual)
        {
            this.idUserActual = idUserActual;
            InitializeComponent();
        }

        private void ListarBorradores_Load(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerBorradores = bd.obtenerStoredProcedure("obtenerBorradores");
            spObtenerBorradores.Parameters.Add("@Id_User", SqlDbType.Int).Value = idUserActual;
            

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerBorradores;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Descripcion"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Monto"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["tipo_public"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Fecha_Final"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = "Modificar";
                dataGridView1.Rows[n].Cells[5].Value = item["Id_Publicacion"].ToString();

            }

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No posee borradores");
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && (e.RowIndex != -1))
            {
                int idPublicacion = (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()));
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "Compra Inmediata")
                {
                    ModificarCompra mc = new ModificarCompra(idPublicacion);
                    mc.Show();
                }
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "Subasta")
                {

                    ModificarSubasta ms = new ModificarSubasta(idPublicacion);
                    ms.Show();
                }
            }
        }
    }
}
