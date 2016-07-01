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
        private int idamodificar;

        public ModificarEmpresa(string idempresa)
        {
            InitializeComponent();
            idamodificar = Convert.ToInt32(idempresa);
        }

        private void ModificarEmpresa_Load(object sender, EventArgs e)
        {
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

        }
    }
}
