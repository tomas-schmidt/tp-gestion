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
        public GenerarCompra()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void GenerarCompra_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_monto,
                txt_Stock
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
        }

        protected override void interactuar()
        {
            try
            {
                int si1 = comboBox1.SelectedIndex;
                int si2 = comboBox2.SelectedIndex;
                BaseDeDatos bd = new BaseDeDatos();
                var spGenerarPublicacion = bd.obtenerStoredProcedure("generarCompra");
                spGenerarPublicacion.Parameters.Add("@Id_User", SqlDbType.Int).Value = 1; // falta id user
                spGenerarPublicacion.Parameters.Add("@Monto", SqlDbType.Float).Value = txt_monto.Text;
                spGenerarPublicacion.Parameters.Add("@Stock", SqlDbType.Int).Value = Convert.ToDouble(txt_Stock.Text);
                spGenerarPublicacion.Parameters.Add("@Visibilidad", SqlDbType.VarChar).Value = (string)comboBox1.Items[si1];
                spGenerarPublicacion.Parameters.Add("@Preguntas", SqlDbType.Bit).Value = checkBox1.CheckState;
                spGenerarPublicacion.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = txt_descripcion.Text;
                spGenerarPublicacion.Parameters.Add("@Estado", SqlDbType.VarChar).Value = (string)comboBox2.Items[si2];
                spGenerarPublicacion.ExecuteNonQuery();
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
    }
}
