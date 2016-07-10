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

namespace WindowsFormsApplication1.Facturas
{
    public partial class ListarFacturas : FormMaestro
    {
        private int idUserActual;

        public ListarFacturas(int idUserActual)
        {
            this.idUserActual = idUserActual;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        protected override void interactuar()
        {
            string consulta;
            consulta = "select f.* from C_HASHTAG.Factura f join C_HASHTAG.Item i on (i.Id_Factura = f.Id_Factura) join C_HASHTAG.Publicacion p on (p.Id_Publicacion = f.Id_Publicacion) where p.Id_User = " + idUserActual + " and f.Fecha < @FechaMaxima and f.Fecha > @FechaMinima";

            if (txt_mayor.Text != "")
            {
                consulta = consulta + " and f.Total > " + txt_mayor.Text;
            }
            if (txt_menor.Text != "")
            {
                consulta = consulta + " and f.Total < " + txt_menor.Text; ;
            }
            if (txt_descripcion.Text != "")
            {
                consulta = consulta + " and i.Descripcion like '%" + txt_descripcion.Text + "%'";
            }

            consulta = consulta + " order by Id_Factura desc";

            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerFacturas = bd.obtenerConsulta(consulta);
            spObtenerFacturas.Parameters.Add("@FechaMaxima", SqlDbType.DateTime).Value = dateTimePicker_menor.Value;
            spObtenerFacturas.Parameters.Add("@FechaMinima", SqlDbType.DateTime).Value = dateTimePicker_mayor.Value;
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerFacturas;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Id_Factura"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Fecha"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Total"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txt_descripcion.Clear();
            txt_mayor.Clear();
            txt_menor.Clear();
        }

    }
}
