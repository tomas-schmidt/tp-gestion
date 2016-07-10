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
    public partial class ModificarVisibilidad : FormMaestro
    {
        private int idAModificar;

        public ModificarVisibilidad(string idVisibilidad)
        {
            InitializeComponent();
            this.idAModificar = Convert.ToInt32(idVisibilidad);
        }

        private void ModificarVisibilidad_Load(object sender, EventArgs e)
        {
            validador.textBoxsDecimales(new List<TextBox>(new[] {
                txt_comEnvio,
                txt_comTipo,
                txt_comProd
            }));

            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_comEnvio,
                txt_comTipo,
                txt_comProd,
                txt_nombre
            }));
            
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerVisibilidad = bd.obtenerStoredProcedure("obtenerVisibilidad");
            spObtenerVisibilidad.Parameters.Add("@Id_Visibilidad", SqlDbType.Int).Value = idAModificar;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerVisibilidad;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                txt_nombre.Text = item["Visibilidad_Desc"].ToString();
                txt_comProd.Text = item["Comision_Prod_Vend"].ToString();
                txt_comTipo.Text = item["Comision_Tipo_Public"].ToString();
                txt_comEnvio.Text = item["Comision_Envio_Prod"].ToString();
                cb_habilitado.Checked = (bool)item["Habilitado"];
            }
        }

        protected override void interactuar()
        {
            try
            {
                BaseDeDatos bd = new BaseDeDatos();
                var spModificarVisibilidad = bd.obtenerStoredProcedure("modificarVisibilidad");
                spModificarVisibilidad.Parameters.Add("@Id_Visibilidad", SqlDbType.Int).Value = idAModificar;
                spModificarVisibilidad.Parameters.Add("@Visibilidad_Desc", SqlDbType.VarChar).Value = txt_nombre.Text;
                spModificarVisibilidad.Parameters.Add("@Comision_Prod_Vend", SqlDbType.Float).Value = Convert.ToDouble(txt_comProd.Text);
                spModificarVisibilidad.Parameters.Add("@Comision_Envio_Prod", SqlDbType.Float).Value = Convert.ToDouble(txt_comEnvio.Text);
                spModificarVisibilidad.Parameters.Add("@Comision_Tipo_Public", SqlDbType.Float).Value = Convert.ToDouble(txt_comTipo.Text);
                spModificarVisibilidad.Parameters.Add("@Habilitado", SqlDbType.Bit).Value = cb_habilitado.CheckState;
                var reader = spModificarVisibilidad.ExecuteReader();
                reader.Read();
                spModificarVisibilidad.Connection.Close();
                MessageBox.Show("Campos actualizados exitosamente");
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
