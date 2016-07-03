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
    public partial class GenerarCompra : FormMaestro
    {
        private int idUser;

        public GenerarCompra(int idUser)
        {
            InitializeComponent();
            this.idUser = idUser;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void GenerarCompra_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_monto,
                txt_Stock,
                txt_descripcion
            }));

            validador.textBoxsNumericos(new List<TextBox>(new[] {
                txt_Stock
            }));

            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerVisibilidades = bd.obtenerStoredProcedure("ObtenerVisibilidades");
            var reader = spObtenerVisibilidades.ExecuteReader();
            while (reader.Read())
            {
                string visibilidad = reader[1].ToString();
                comboBox1.Items.Add(visibilidad);
            }

            var spObtenerEstados = bd.obtenerStoredProcedure("obtenerEstadosElegibles");
            var reader2 = spObtenerEstados.ExecuteReader();
            while (reader2.Read())
            {
                string estado = reader2[1].ToString();
                comboBox2.Items.Add(estado);
            }

            var spObtenerRubros = bd.obtenerStoredProcedure("obtenerRubros");

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerRubros;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = "false";
                dataGridView1.Rows[n].Cells[1].Value = item["Desc_Corta"].ToString();
            }

        }

        protected override void interactuar()
        {
            try
            {
                int si1 = comboBox1.SelectedIndex;
                int si2 = comboBox2.SelectedIndex;
                BaseDeDatos bd = new BaseDeDatos();
                var spGenerarPublicacion = bd.obtenerStoredProcedure("generarCompra");
                spGenerarPublicacion.Parameters.Add("@Id_User", SqlDbType.Int).Value = this.idUser;
                spGenerarPublicacion.Parameters.Add("@Monto", SqlDbType.Float).Value = txt_monto.Text;
                spGenerarPublicacion.Parameters.Add("@Stock", SqlDbType.Int).Value = Convert.ToDouble(txt_Stock.Text);
                spGenerarPublicacion.Parameters.Add("@Visibilidad", SqlDbType.VarChar).Value = (string)comboBox1.Items[si1];
                spGenerarPublicacion.Parameters.Add("@Preguntas", SqlDbType.Bit).Value = checkBox1.CheckState;
                spGenerarPublicacion.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = txt_descripcion.Text;
                spGenerarPublicacion.Parameters.Add("@Estado", SqlDbType.VarChar).Value = (string)comboBox2.Items[si2];
                var reader = spGenerarPublicacion.ExecuteReader();
                reader.Read();
                

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (bool.Parse(item.Cells[0].Value.ToString()))
                    {
                        var spAgregarRubroAPublicacion = bd.obtenerStoredProcedure("agregarRubroAPublicacion");
                        spAgregarRubroAPublicacion.Parameters.Add("@Rubro", SqlDbType.VarChar).Value = item.Cells[1].Value.ToString();
                        spAgregarRubroAPublicacion.Parameters.Add("@Id_Publicacion", SqlDbType.Int).Value = reader[0];
                        spAgregarRubroAPublicacion.ExecuteNonQuery();
                        spAgregarRubroAPublicacion.Connection.Close();
                    }
                }
                spGenerarPublicacion.Connection.Close();
                MessageBox.Show("Nueva publicacion generada");
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
            }
        }

        private void btn_generarCompra_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
