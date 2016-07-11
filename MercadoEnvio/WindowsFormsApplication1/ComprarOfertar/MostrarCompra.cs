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
    public partial class MostrarCompra : FormMaestro
    {
        private int idPublicacion;
        private int idUserActual;

        public MostrarCompra(int idPublicacion, int idUserActual)
        {
            this.idPublicacion = idPublicacion;
            this.idUserActual = idUserActual;
            InitializeComponent();
        }

        private void MostrarCompra_Load(object sender, EventArgs e)
        {
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
                txt_stock.Text = item["Stock"].ToString();
                txt_fechaFinal.Text = item["Fecha_Final"].ToString();
                checkBox1.Checked = ((bool)item["Envio"]);
                checkBox1.Enabled = false;
                for (int i = 1; i <= (Convert.ToInt32(item["Stock"].ToString())); i++)
                {
                    cb_Stock.Items.Add(i);
                }
            }

            if (cb_Stock.Items.Count == 0)
            {
                cb_Stock.Items.Add(0);
                cb_Stock.SelectedItem = cb_Stock.Items[0];
            }
            else
            {
                cb_Stock.SelectedItem = cb_Stock.Items[0];
            }

        }

        protected override void interactuar()
        {
            try
            {
                int si = cb_Stock.SelectedIndex;
                BaseDeDatos bd = new BaseDeDatos();
                var spRealizarCompra = bd.obtenerStoredProcedure("realizarCompra");
                spRealizarCompra.Parameters.Add("@Id_Publicacion", SqlDbType.Int).Value = idPublicacion;
                spRealizarCompra.Parameters.Add("@Stock", SqlDbType.Int).Value = Convert.ToInt32(cb_Stock.Items[si]);
                spRealizarCompra.Parameters.Add("@Id_User", SqlDbType.Int).Value = idUserActual;
                spRealizarCompra.ExecuteNonQuery();
                spRealizarCompra.Connection.Close();
                MessageBox.Show("Compra realizada exitosamente");
                this.Close();
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
            }
        }

        private void btn_comprar_Click(object sender, EventArgs e)
        {
            this.submitir();
        }
    }
}
