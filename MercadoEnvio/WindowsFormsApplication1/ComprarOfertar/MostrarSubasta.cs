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
    public partial class MostrarSubasta : FormMaestro
    {
        private int idPublicacion;
        private int idUserActual;

        public MostrarSubasta(int idPublicacion, int idUserActual)
        {
            this.idPublicacion = idPublicacion;
            this.idUserActual = idUserActual;
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void MostrarSubasta_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_nuevoMonto
            }));

            validador.textBoxsNumericos(new List<TextBox>(new[] {
                txt_nuevoMonto
            }));
            
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerPublicacion = bd.obtenerStoredProcedure("obtenerPublicacion");
            spObtenerPublicacion.Parameters.Add("@Id_Publicacion", SqlDbType.Int).Value = idPublicacion;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerPublicacion;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                txt_monto.Text = item["Monto"].ToString();
                txt_descripcion.Text = item["Descripcion"].ToString();
                txt_fechaFinal.Text = item["Fecha_Final"].ToString();
                checkBox1.Checked = ((bool)item["Envio"]);
                checkBox1.Enabled = false;
            }

        }

        private void btn_comprar_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        protected override void interactuar()
        {
            try
            {
                BaseDeDatos bd = new BaseDeDatos();
                var spRealizarOferta = bd.obtenerStoredProcedure("realizarOferta");
                spRealizarOferta.Parameters.Add("@Id_Publicacion", SqlDbType.Int).Value = idPublicacion;
                spRealizarOferta.Parameters.Add("@MontoOfertado", SqlDbType.Int).Value = Convert.ToInt32(txt_nuevoMonto.Text);
                spRealizarOferta.Parameters.Add("@Id_User", SqlDbType.Int).Value = idUserActual;
                spRealizarOferta.ExecuteNonQuery();
                spRealizarOferta.Connection.Close();
                MessageBox.Show("Oferta realizada exitosamente");
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
            }
        }

    }
}
