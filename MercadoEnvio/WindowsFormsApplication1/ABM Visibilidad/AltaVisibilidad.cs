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
    public partial class AltaVisibilidad : FormMaestro
    {
        public AltaVisibilidad()
        {
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void AltaVisibilidad_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_comEnvio,
                txt_comTipo,
                txt_comProd,
                txt_nombre
            }));

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        protected override void interactuar()
        {
            try
            {
                BaseDeDatos bd = new BaseDeDatos();
                var spCrearVisibilidad = bd.obtenerStoredProcedure("crearVisibilidad");
                spCrearVisibilidad.Parameters.Add("@Visibilidad_Desc", SqlDbType.VarChar).Value = txt_nombre.Text;
                spCrearVisibilidad.Parameters.Add("@Comision_Prod_Vend", SqlDbType.Float).Value = Convert.ToDouble(txt_comProd.Text);
                spCrearVisibilidad.Parameters.Add("@Comision_Envio_Prod", SqlDbType.Float).Value = Convert.ToDouble(txt_comEnvio.Text);
                spCrearVisibilidad.Parameters.Add("@Comision_Tipo_Public", SqlDbType.Float).Value = Convert.ToDouble(txt_comTipo.Text);
                spCrearVisibilidad.Parameters.Add("@Habilitado", SqlDbType.Bit).Value = cb_habilitado.CheckState;
                var reader = spCrearVisibilidad.ExecuteReader();
                reader.Read();
                spCrearVisibilidad.Connection.Close();
                MessageBox.Show("Nueva visibilidad creada exitosamente");
                txt_comProd.Clear();
                txt_comTipo.Clear();
                txt_nombre.Clear();
                txt_comEnvio.Clear(); 
                cb_habilitado.CheckState = 0;
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.submitir();
        }
    }
}
