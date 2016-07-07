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
    public partial class MostrarCompra : Form
    {
        private int idPublicacion;

        public MostrarCompra(int idPublicacion)
        {
            this.idPublicacion = idPublicacion;
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
                for (int i = 1; i <= (Convert.ToInt32(item["Stock"].ToString())); i++)
                {
                    cb_Stock.Items.Add(i);
                }
            }
            cb_Stock.SelectedItem = cb_Stock.Items[0];


        }
    }
}
