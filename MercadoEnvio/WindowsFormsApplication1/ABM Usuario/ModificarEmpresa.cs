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
    public partial class ModificarEmpresa : Form
    {
        private Validador validador = new Validador();
        private int idamodificar;

        public ModificarEmpresa(string idempresa)
        {
            InitializeComponent();
            idamodificar = Convert.ToInt32(idempresa);
        }

        private void ModificarEmpresa_Load(object sender, EventArgs e)
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
                txt_nroCalle
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
;
            var spObtenerRubroDeEmpresa = bd.obtenerStoredProcedure("ObtenerRubroDeEmpresa");
            spObtenerRubroDeEmpresa.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = idamodificar;
            var reader2 = spObtenerRubroDeEmpresa.ExecuteReader();
            reader2.Read();
            comboBox1.SelectedItem = reader2[1];

            var spObtenerEmpresaYUsername = bd.obtenerStoredProcedure("ObtenerEmpresaYUsername");
            spObtenerEmpresaYUsername.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = idamodificar;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerEmpresaYUsername;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                txt_username.Text = item["Username"].ToString();
                txt_cuit.Text = item["Cuit"].ToString();
                txt_razonSocial.Text = item["Razon_Social"].ToString();
                txt_Ciudad.Text = item["Ciudad"].ToString();
                txt_mail.Text = item["Mail"].ToString();
                txt_calle.Text = item["Calle"].ToString();
                txt_nroCalle.Text = item["Nro_Calle"].ToString();
                txt_localidad.Text = item["Localidad"].ToString();
                txt_nombreContacto.Text = item["Nombre_Contacto"].ToString();
                txt_codPostal.Text = item["Cod_Postal"].ToString();
                txt_nroPiso.Text = item["Piso"].ToString();
                txt_depto.Text = item["Departamento"].ToString();
                txt_telefono.Text = item["Telefono"].ToString();
            }
        }

        private void btn_crearEmpesa_Click(object sender, EventArgs e)
        {
            try
            {
                validador.validar();
                try
                {
                    BaseDeDatos bd = new BaseDeDatos();
                    int si = comboBox1.SelectedIndex;
                    var spModificarEmpresa = bd.obtenerStoredProcedure("modificarEmpresa");
                    spModificarEmpresa.Parameters.Add("@Username", SqlDbType.VarChar).Value = txt_username.Text;
                    spModificarEmpresa.Parameters.Add("@Razon_Social", SqlDbType.VarChar).Value = txt_razonSocial.Text;
                    spModificarEmpresa.Parameters.Add("@Ciudad", SqlDbType.VarChar).Value = txt_Ciudad.Text;
                    spModificarEmpresa.Parameters.Add("@Cuit", SqlDbType.VarChar).Value = txt_cuit.Text;
                    spModificarEmpresa.Parameters.Add("@Mail", SqlDbType.VarChar).Value = txt_mail.Text;
                    spModificarEmpresa.Parameters.Add("@Telefono", SqlDbType.Int).Value = Convert.ToInt32(txt_telefono.Text);
                    spModificarEmpresa.Parameters.Add("@Calle", SqlDbType.VarChar).Value = txt_calle.Text;
                    spModificarEmpresa.Parameters.Add("@Nro_Calle", SqlDbType.Int).Value = Convert.ToInt32(txt_nroCalle.Text);
                    spModificarEmpresa.Parameters.Add("@Piso", SqlDbType.Int).Value = Convert.ToInt32(txt_nroPiso.Text);
                    spModificarEmpresa.Parameters.Add("@Localidad", SqlDbType.VarChar).Value = txt_localidad.Text;
                    spModificarEmpresa.Parameters.Add("@Departamento", SqlDbType.VarChar).Value = txt_depto.Text;
                    spModificarEmpresa.Parameters.Add("@Cod_Postal", SqlDbType.Int).Value = Convert.ToInt32(txt_codPostal.Text);
                    spModificarEmpresa.Parameters.Add("@Rubro_Principal", SqlDbType.VarChar).Value = (string)comboBox1.Items[si];
                    spModificarEmpresa.Parameters.Add("@Nombre_Contacto", SqlDbType.VarChar).Value = txt_nombreContacto.Text;
                    var reader = spModificarEmpresa.ExecuteReader();
                    reader.Read();
                    spModificarEmpresa.Connection.Close();
                    MessageBox.Show("Campos actualizados exitosamente");
                }
                catch (SqlException excepcion)
                {
                    MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
                }
            }
            catch (ValidacionException ve)
            {
                MessageBox.Show(ve.Message);
            }
        }
    }
}
