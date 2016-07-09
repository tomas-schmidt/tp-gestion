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

namespace WindowsFormsApplication1.Calificar
{
    public partial class Calificar : Form
    {
        private int idCompra;

        public Calificar(int idCompra)
        {
            this.idCompra = idCompra;
            InitializeComponent();
        }

        private void Calificar_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 5; i++)
            {
                cb_cantEstrellas.Items.Add(i);
            }
            cb_cantEstrellas.SelectedItem = cb_cantEstrellas.Items[0];
        }

        private void btn_calificar_Click(object sender, EventArgs e)
        {
            try
            {
                int si = cb_cantEstrellas.SelectedIndex;
                BaseDeDatos bd = new BaseDeDatos();
                var spCalificar = bd.obtenerStoredProcedure("Calificar");
                spCalificar.Parameters.Add("@Id_Compra", SqlDbType.Int).Value = idCompra;
                spCalificar.Parameters.Add("@Cant_Estrellas", SqlDbType.Int).Value = Convert.ToInt32(cb_cantEstrellas.Items[si]);
                spCalificar.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = txt_comentario.Text;
                spCalificar.ExecuteNonQuery();
                spCalificar.Connection.Close();
                MessageBox.Show("Calificacion realizada exitosamente");
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
            }
        }
    }
}
