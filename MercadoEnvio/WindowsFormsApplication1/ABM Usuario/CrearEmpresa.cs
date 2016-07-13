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

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class CrearEmpresa : FormMaestro
    {
        public CrearEmpresa()
        {
            InitializeComponent();
        }

        private void txt_calle_TextChanged(object sender, EventArgs e)
        {

        }

        protected override void interactuar()
        {
            try
            {
                BaseDeDatos bd = new BaseDeDatos();
                int si = comboBox1.SelectedIndex;
                var spcrearUsuarioYEmpresa = bd.obtenerStoredProcedure("crearUsuarioYEmpresa");
                spcrearUsuarioYEmpresa.Parameters.Add("@Username", SqlDbType.VarChar).Value = txt_username.Text;
                spcrearUsuarioYEmpresa.Parameters.Add("@Contraseña", SqlDbType.VarChar).Value = SHA256.GetSHA256(txt_password.Text);
                spcrearUsuarioYEmpresa.Parameters.Add("@Razon_Social", SqlDbType.VarChar).Value = txt_razonSocial.Text;
                spcrearUsuarioYEmpresa.Parameters.Add("@Ciudad", SqlDbType.VarChar).Value = txt_Ciudad.Text;
                spcrearUsuarioYEmpresa.Parameters.Add("@Cuit", SqlDbType.VarChar).Value = txt_cuit.Text;
                spcrearUsuarioYEmpresa.Parameters.Add("@Mail", SqlDbType.VarChar).Value = txt_mail.Text;
                spcrearUsuarioYEmpresa.Parameters.Add("@Telefono", SqlDbType.Int).Value = Convert.ToInt32(txt_telefono.Text);
                spcrearUsuarioYEmpresa.Parameters.Add("@Calle", SqlDbType.VarChar).Value = txt_calle.Text;
                spcrearUsuarioYEmpresa.Parameters.Add("@Nro_Calle", SqlDbType.Int).Value = Convert.ToInt32(txt_nroCalle.Text);
                spcrearUsuarioYEmpresa.Parameters.Add("@Piso", SqlDbType.Int).Value = Convert.ToInt32(txt_nroPiso.Text);
                spcrearUsuarioYEmpresa.Parameters.Add("@Localidad", SqlDbType.VarChar).Value = txt_localidad.Text;
                spcrearUsuarioYEmpresa.Parameters.Add("@Departamento", SqlDbType.VarChar).Value = txt_depto.Text;
                spcrearUsuarioYEmpresa.Parameters.Add("@Cod_Postal", SqlDbType.Int).Value = Convert.ToInt32(txt_codPostal.Text);
                spcrearUsuarioYEmpresa.Parameters.Add("@Rubro_Principal", SqlDbType.VarChar).Value = (string)comboBox1.Items[si];
                spcrearUsuarioYEmpresa.Parameters.Add("@Nombre_Contacto", SqlDbType.VarChar).Value = txt_nombreContacto.Text;
                var reader = spcrearUsuarioYEmpresa.ExecuteReader();
                reader.Read();
                spcrearUsuarioYEmpresa.Connection.Close();
                MessageBox.Show("Nueva empresa cargada con éxito");
                this.Close();
            }

            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void btn_crearEmpesa_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        private void CrearEmpresa_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_calle,
                txt_Ciudad,
                txt_codPostal,
                txt_cuit,
                txt_localidad,
                txt_mail,
                txt_nombreContacto,
                txt_telefono,
                txt_razonSocial,
                txt_nroCalle,
                txt_username,
                txt_password
            }));

            validador.textBoxsNumericos(new List<TextBox>(new[] {
                txt_codPostal,
                txt_telefono,
                txt_nroCalle,
                txt_nroPiso
            }));

            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerRubros = bd.obtenerStoredProcedure("obtenerRubros");
            var reader = spObtenerRubros.ExecuteReader();
            while (reader.Read())
            {
                string rubro = reader[1].ToString();
                comboBox1.Items.Add(rubro);
            }
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
