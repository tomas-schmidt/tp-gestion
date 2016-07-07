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

namespace WindowsFormsApplication1.ComprarOfertar
{
    public partial class ListarPublicaciones : FormMaestro
    {
        private int idUserActual;

        public ListarPublicaciones(int idUserActual)
        {
            InitializeComponent();
            this.idUserActual = idUserActual;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                int idPublicacion = (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()));
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "Compra Inmediata")
                {

                    MostrarCompra mc = new MostrarCompra(idPublicacion);
                    mc.Show();
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        protected override void interactuar()
        {
            try
            {
                BaseDeDatos bd = new BaseDeDatos();
                DataTable dbdataset2 = new DataTable();
                foreach (DataGridViewRow item in dataGridView2.Rows)
                {
                    if (bool.Parse(item.Cells[0].Value.ToString()))
                    {
                        var spObtenerPublicaciones = bd.obtenerStoredProcedure("obtenerPublicaciones");
                        spObtenerPublicaciones.Parameters.Add("@Rubro", SqlDbType.VarChar).Value = item.Cells[1].Value.ToString();
                        spObtenerPublicaciones.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = txt_descripcion.Text;
                        //var reader = spObtenerPublicaciones.ExecuteReader();
                        //reader.Read();

                        SqlDataAdapter sda2 = new SqlDataAdapter();
                        sda2.SelectCommand = spObtenerPublicaciones;
                        
                        sda2.Fill(dbdataset2);
                    }
                }
                foreach (DataRow item2 in dbdataset2.Rows)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = item2["Descripcion"].ToString();
                            dataGridView1.Rows[n].Cells[1].Value = item2["Monto"].ToString();
                            dataGridView1.Rows[n].Cells[2].Value = item2["tipo_public"].ToString();
                            dataGridView1.Rows[n].Cells[4].Value = item2["Id_Publicacion"].ToString();
                            dataGridView1.Rows[n].Cells[3].Value = "Ver";
                        }
                    }
                
            
            catch (SqlException excepcion)
            {
                MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
            }
        }

        private void ListarPublicaciones_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_descripcion
            }));

            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerRubros = bd.obtenerStoredProcedure("obtenerRubros");

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerRubros;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = "false";
                dataGridView2.Rows[n].Cells[1].Value = item["Desc_Corta"].ToString();
            }
            spObtenerRubros.Connection.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            this.submitir();
        }
    }
}
