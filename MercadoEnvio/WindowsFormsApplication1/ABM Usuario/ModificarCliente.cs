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
    public partial class ModificarCliente : FormMaestro
    {
        private int idamodificar;

        public ModificarCliente(string idcliente)
        {
            InitializeComponent();
            idamodificar = Convert.ToInt32(idcliente);
        }

        private void ModificarCliente_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_calle,
                txt_nombre,
                txt_apellido,
                txt_codPostal,
                txt_localidad,
                txt_mail,
                txt_telefono,
                txt_nroCalle,
                txt_nroDoc,
            }));

            validador.textBoxsNumericos(new List<TextBox>(new[] {
                txt_codPostal,
                txt_telefono,
                txt_nroCalle,
                txt_nroPiso
            }));





            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerDocs = bd.obtenerStoredProcedure("obtenerTiposDocumento");
            var reader = spObtenerDocs.ExecuteReader();
            while (reader.Read())
            {
                string doc = reader[1].ToString();
                comboBox1.Items.Add(doc);
            }
            ;

            var spObtenerDocDeCliente = bd.obtenerStoredProcedure("obtenerDocDeCliente");
            spObtenerDocDeCliente.Parameters.Add("@Id_Cliente", SqlDbType.Int).Value = idamodificar;
            var reader2 = spObtenerDocDeCliente.ExecuteReader();
            reader2.Read();
            comboBox1.SelectedItem = reader2[1];



            var spObtenerClienteYUsername = bd.obtenerStoredProcedure("obtenerClienteYUsername");
            spObtenerClienteYUsername.Parameters.Add("@Id_Cliente", SqlDbType.Int).Value = idamodificar;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerClienteYUsername;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                txt_username.Text = item["Username"].ToString();
                txt_nombre.Text = item["Nombre"].ToString();
                txt_apellido.Text = item["Apellido"].ToString();
                txt_mail.Text = item["Mail"].ToString();
                txt_calle.Text = item["Domicilio_Calle"].ToString();
                txt_nroCalle.Text = item["Nro_Calle"].ToString();
                txt_localidad.Text = item["Localidad"].ToString();
                txt_codPostal.Text = item["Cod_Postal"].ToString();
                txt_nroPiso.Text = item["Piso"].ToString();
                txt_depto.Text = item["Departamento"].ToString();
                txt_telefono.Text = item["Telefono"].ToString();
                txt_nroDoc.Text = item["Nro_Doc"].ToString();
                dateTimePicker1.Value = (DateTime)item["Nacimiento"];
            }
        }

        protected override void interactuar()
        {
            try
            {
                BaseDeDatos bd = new BaseDeDatos();
                int si = comboBox1.SelectedIndex;
                var spModificarCliente = bd.obtenerStoredProcedure("modificarCliente");
                spModificarCliente.Parameters.Add("@Username", SqlDbType.VarChar).Value = txt_username.Text;
                spModificarCliente.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txt_nombre.Text;
                spModificarCliente.Parameters.Add("@Apellido", SqlDbType.VarChar).Value = txt_apellido.Text;
                spModificarCliente.Parameters.Add("@Nro_Doc", SqlDbType.VarChar).Value = txt_nroDoc.Text;
                spModificarCliente.Parameters.Add("@Mail", SqlDbType.VarChar).Value = txt_mail.Text;
                spModificarCliente.Parameters.Add("@Telefono", SqlDbType.Int).Value = Convert.ToInt32(txt_telefono.Text);
                spModificarCliente.Parameters.Add("@Domicilio_Calle", SqlDbType.VarChar).Value = txt_calle.Text;
                spModificarCliente.Parameters.Add("@Nro_Calle", SqlDbType.Int).Value = Convert.ToInt32(txt_nroCalle.Text);
                spModificarCliente.Parameters.Add("@Piso", SqlDbType.Int).Value = Convert.ToInt32(txt_nroPiso.Text);
                spModificarCliente.Parameters.Add("@Localidad", SqlDbType.VarChar).Value = txt_localidad.Text;
                spModificarCliente.Parameters.Add("@Departamento", SqlDbType.VarChar).Value = txt_depto.Text;
                spModificarCliente.Parameters.Add("@Cod_Postal", SqlDbType.Int).Value = Convert.ToInt32(txt_codPostal.Text);
                spModificarCliente.Parameters.Add("@Tipo_Doc", SqlDbType.VarChar).Value = (string)comboBox1.Items[si];
                spModificarCliente.Parameters.Add("@Nacimiento", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                var reader = spModificarCliente.ExecuteReader();
                reader.Read();
                spModificarCliente.Connection.Close();
                MessageBox.Show("Campos actualizados exitosamente");
                this.Close();
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }



        private void btn_actualizarCliente_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CambiarContrasenia cc = new CambiarContrasenia(txt_username.Text);
            cc.Show();
        }
    }
}
