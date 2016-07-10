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

namespace WindowsFormsApplication1
{
    public partial class CambiarContraseniaUser : FormMaestro
    {
        private int idUserActual;

        public CambiarContraseniaUser(int idUserActual)
        {
            this.idUserActual = idUserActual;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        protected override void interactuar()
        {
            try
            {
                BaseDeDatos bd = new BaseDeDatos();
                var spCambiarContraseniaPorAdmin = bd.obtenerStoredProcedure("cambiarContraseniaPorUser");
                spCambiarContraseniaPorAdmin.Parameters.Add("@Id_User", SqlDbType.Int).Value = idUserActual;
                spCambiarContraseniaPorAdmin.Parameters.Add("@ContraseniaVieja", SqlDbType.VarChar).Value = SHA256.GetSHA256(txt_contraseniaVieja.Text);
                spCambiarContraseniaPorAdmin.Parameters.Add("@Contrasenia1", SqlDbType.VarChar).Value = SHA256.GetSHA256(txt_contrasenia1.Text);
                spCambiarContraseniaPorAdmin.Parameters.Add("@Contrasenia2", SqlDbType.VarChar).Value = SHA256.GetSHA256(txt_contrasenia2.Text);
                spCambiarContraseniaPorAdmin.ExecuteNonQuery();

                spCambiarContraseniaPorAdmin.Connection.Close();
                MessageBox.Show("Contraseña cambiada con éxito");
            }

            catch (SqlException excepcion)
            {
                MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
            }

        }

        private void CambiarContraseniaUser_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_contraseniaVieja,
                txt_contrasenia1,
                txt_contrasenia2
            }));
        }
    }
}
