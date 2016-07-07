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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void lbl_nombre_Click(object sender, EventArgs e)
        {

        }

        private void txt_passwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                BaseDeDatos bd = new BaseDeDatos();
                var spLogin = bd.obtenerStoredProcedure("Login");
                spLogin.Parameters.Add("@Username", SqlDbType.VarChar).Value = txt_user.Text;
                spLogin.Parameters.Add("@Contraseña", SqlDbType.VarChar).Value = SHA256.GetSHA256(txt_passwd.Text);
                var reader = spLogin.ExecuteReader();
                reader.Read();
                int iduser = Convert.ToInt32(reader[0]);
                RolesUsuario f = new RolesUsuario(iduser);
                f.Show();

            }
            catch (SqlException excepcion)
            {
                MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
            }
        
        }
    }
}
