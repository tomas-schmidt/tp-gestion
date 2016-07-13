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
            validador.textBoxsNumericos(new List<TextBox>(new[] {
                txt_mayor,
                txt_menor
            }));


            int date = 20150101;

            int d = date % 100;
            int m = (date / 100) % 100;
            int y = date / 10000;

            var result = new DateTime(y, m, d);
            dateTimePicker_mayor.Value = result;

            int date2 = 20170101;

            int d2 = date2 % 100;
            int m2 = (date2 / 100) % 100;
            int y2 = date2 / 10000;

            var result2 = new DateTime(y2, m2, d2);


            dateTimePicker_menor.Value = result2;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        protected override void interactuar()
        {
            dataGridView1.Rows.Clear();
            cb_pags.Items.Clear();

            string consulta;
            consulta = "select distinct f.* from C_HASHTAG.Factura f join C_HASHTAG.Item i on (i.Id_Factura = f.Id_Factura) join C_HASHTAG.Publicacion p on (p.Id_Publicacion = f.Id_Publicacion) where p.Id_User = " + idUserActual + " and f.Fecha < @FechaMaxima and f.Fecha > @FechaMinima";

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

            consulta = consulta + " order by Fecha desc";

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
                dataGridView1.Rows[n].Visible = false;
            }

            //cb_pags.Items = 0;

            double rows = ((dataGridView1.Rows.Count));
            double paginas = Math.Ceiling(rows / 10);
            /*if (rows > 0)
            {
                for (int j = 0; j < rows; j++)
                {
                    dataGridView1.Rows[j].Visible = false;
                }
            }*/

            if (paginas > 0)
            {
                for (int i = 1; i <= paginas; i++)
                {
                    cb_pags.Items.Add(i);
                }
                cb_pags.SelectedItem = cb_pags.Items[0];
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            txt_descripcion.Clear();
            txt_mayor.Clear();
            txt_menor.Clear();
            cb_pags.Items.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cb_pags_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rows = ((dataGridView1.Rows.Count));

            if (rows > 0)
            {
                for (int j = 0; j < rows; j++)
                {
                    dataGridView1.Rows[j].Visible = false;
                }
            }

            int pag = (((Convert.ToInt32(cb_pags.SelectedIndex)) + 1) * 10);
            for (int i = pag - 10; i < pag; i++)
            {
                if (dataGridView1.Rows.Count > i)
                {
                    dataGridView1.Rows[i].Visible = true;
                }
            }
        }

    }
}
