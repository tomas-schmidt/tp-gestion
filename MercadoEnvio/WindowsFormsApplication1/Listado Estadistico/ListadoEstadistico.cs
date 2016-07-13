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

namespace WindowsFormsApplication1.Listado_Estadistico
{
    public partial class ListadoEstadistico : Form
    {
        public ListadoEstadistico()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cb_tipoListado.Items.Add("Vendedores con mayor cantidad de productos no vendidos");

            cb_tipoListado.Items.Add("Clientes con mayor cantidad de productos comprados");

            cb_tipoListado.Items.Add("Vendedores con mayor cantidad de facturas");

            cb_tipoListado.Items.Add("Vendedores con mayor monto facturado");

            cb_trimestre.Items.Add("1");
            cb_trimestre.Items.Add("2");
            cb_trimestre.Items.Add("3");
            cb_trimestre.Items.Add("4");

            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerRubros = bd.obtenerStoredProcedure("obtenerRubros");
            var reader = spObtenerRubros.ExecuteReader();
            while (reader.Read())
            {
                string rubro = reader[1].ToString();
                cb_rubros.Items.Add(rubro);
            }

            cb_visibilidad.Items.Add("Todas");
            var spObtenerVisibilidades = bd.obtenerStoredProcedure("ObtenerVisibilidades");
            var reader2 = spObtenerVisibilidades.ExecuteReader();
            while (reader2.Read())
            {
                string visibilidad = reader2[1].ToString();
                cb_visibilidad.Items.Add(visibilidad);
            }

            cb_rubros.SelectedItem = cb_rubros.Items[0];
            cb_trimestre.SelectedItem = cb_trimestre.Items[0];
            cb_tipoListado.SelectedItem = cb_tipoListado.Items[0];
            cb_visibilidad.SelectedItem = cb_visibilidad.Items[0];

            cb_rubros.Hide();
            cb_visibilidad.Show();
            lbl_rubro.Hide();
            lbl_visibilidad.Show();
            dataGridView1.Show();
            dataGridView2.Hide();
            dataGridView3.Hide();
            dataGridView4.Hide();
            btn_mayorCantFacturas.Hide();
            btn_mayorMontoFacturas.Hide();
            btn_mayorProdsComprados.Hide();
            btn_prodsNoVendidos.Show();


        }





        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void cb_tipoListado_SelectedIndexChanged(object sender, EventArgs e)
        {
            int si1 = cb_tipoListado.SelectedIndex;
            if ((string)cb_tipoListado.Items[si1] == "Vendedores con mayor cantidad de productos no vendidos")
            {
                cb_rubros.Hide();
                cb_visibilidad.Show();
                lbl_rubro.Hide();
                lbl_visibilidad.Show();
                dataGridView1.Show();
                dataGridView2.Hide();
                dataGridView3.Hide();
                dataGridView4.Hide();
                btn_mayorCantFacturas.Hide();
                btn_mayorMontoFacturas.Hide();
                btn_mayorProdsComprados.Hide();
                btn_prodsNoVendidos.Show();
            }

            if ((string)cb_tipoListado.Items[si1] == "Clientes con mayor cantidad de productos comprados")
            {
                cb_rubros.Show();
                cb_visibilidad.Hide();
                lbl_rubro.Show();
                lbl_visibilidad.Hide();
                dataGridView1.Hide();
                dataGridView2.Show();
                dataGridView3.Hide();
                dataGridView4.Hide();
                btn_mayorCantFacturas.Hide();
                btn_mayorMontoFacturas.Hide();
                btn_mayorProdsComprados.Show();
                btn_prodsNoVendidos.Hide();
            }

            if ((string)cb_tipoListado.Items[si1] == "Vendedores con mayor cantidad de facturas")
            {
                cb_rubros.Hide();
                cb_visibilidad.Hide();
                lbl_rubro.Hide();
                lbl_visibilidad.Hide();
                dataGridView1.Hide();
                dataGridView2.Hide();
                dataGridView3.Show();
                dataGridView4.Hide();
                btn_mayorCantFacturas.Show();
                btn_mayorMontoFacturas.Hide();
                btn_mayorProdsComprados.Hide();
                btn_prodsNoVendidos.Hide();
            }

            if ((string)cb_tipoListado.Items[si1] == "Vendedores con mayor monto facturado")
            {
                cb_rubros.Hide();
                cb_visibilidad.Hide();
                lbl_rubro.Hide();
                lbl_visibilidad.Hide();
                dataGridView1.Hide();
                dataGridView2.Hide();
                dataGridView3.Hide();
                dataGridView4.Show();
                btn_mayorCantFacturas.Hide();
                btn_mayorMontoFacturas.Show();
                btn_mayorProdsComprados.Hide();
                btn_prodsNoVendidos.Hide();
            }
        }

        private void btn_prodsNoVendidos_Click(object sender, EventArgs e)
        {
            if (txt_anio.Text == "")
            {
                MessageBox.Show("Ingrese un año");
                return;
            }
            try
            {
                dataGridView1.Rows.Clear();
                int si = cb_trimestre.SelectedIndex;
                int si2 = cb_visibilidad.SelectedIndex;
                BaseDeDatos bd = new BaseDeDatos();
                var spVendedoresConMayorCantDeProdsNoVendidos = bd.obtenerStoredProcedure("vendedoresConMayorCantDeProdsNoVendidos");
                spVendedoresConMayorCantDeProdsNoVendidos.Parameters.Add("@anio", SqlDbType.Int).Value = Convert.ToInt32(txt_anio.Text);
                spVendedoresConMayorCantDeProdsNoVendidos.Parameters.Add("@trimestre", SqlDbType.Int).Value = Convert.ToInt32(cb_trimestre.Items[si]);
                spVendedoresConMayorCantDeProdsNoVendidos.Parameters.Add("@visibilidad", SqlDbType.VarChar).Value = (cb_visibilidad.Items[si2]).ToString();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = spVendedoresConMayorCantDeProdsNoVendidos;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                foreach (DataRow item in dbdataset.Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["Username"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = item["Descripcion"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = item["Stock"].ToString();
                    dataGridView1.Rows[n].Cells[3].Value = item["Visibilidad"].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = item["Fecha_Final"].ToString();    
                }
                spVendedoresConMayorCantDeProdsNoVendidos.Connection.Close();
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void btn_mayorProdsComprados_Click(object sender, EventArgs e)
        {
            if (txt_anio.Text == "")
            {
                MessageBox.Show("Ingrese un año");
                return;
            }
            try
            {
                dataGridView2.Rows.Clear();
                int si = cb_trimestre.SelectedIndex;
                int si2 = cb_rubros.SelectedIndex;
                BaseDeDatos bd = new BaseDeDatos();
                var spClientesConMayorCantDeProdsComprados = bd.obtenerStoredProcedure("clientesConMayorCantDeProdsComprados");
                spClientesConMayorCantDeProdsComprados.Parameters.Add("@anio", SqlDbType.Int).Value = Convert.ToInt32(txt_anio.Text);
                spClientesConMayorCantDeProdsComprados.Parameters.Add("@trimestre", SqlDbType.Int).Value = Convert.ToInt32(cb_trimestre.Items[si]);
                spClientesConMayorCantDeProdsComprados.Parameters.Add("@rubro", SqlDbType.VarChar).Value = (cb_rubros.Items[si2]).ToString();

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = spClientesConMayorCantDeProdsComprados;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                foreach (DataRow item in dbdataset.Rows)
                {
                    int n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells[0].Value = item["Username"].ToString();
                    dataGridView2.Rows[n].Cells[1].Value = item["CantidadProductos"].ToString();
                    dataGridView2.Rows[n].Cells[2].Value = item["Rubro"].ToString();
                }
                spClientesConMayorCantDeProdsComprados.Connection.Close();
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }

        }

        private void btn_mayorCantFacturas_Click(object sender, EventArgs e)
        {
            if (txt_anio.Text == "")
            {
                MessageBox.Show("Ingrese un año");
                return;
            }
            try
            {
                dataGridView3.Rows.Clear();
                int si = cb_trimestre.SelectedIndex;
                BaseDeDatos bd = new BaseDeDatos();
                var spVendedoresConMayorCantDeFacturas = bd.obtenerStoredProcedure("vendedoresConMayorCantDeFacturas");
                spVendedoresConMayorCantDeFacturas.Parameters.Add("@anio", SqlDbType.Int).Value = Convert.ToInt32(txt_anio.Text);
                spVendedoresConMayorCantDeFacturas.Parameters.Add("@trimestre", SqlDbType.Int).Value = Convert.ToInt32(cb_trimestre.Items[si]);
                
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = spVendedoresConMayorCantDeFacturas;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                foreach (DataRow item in dbdataset.Rows)
                {
                    int n = dataGridView3.Rows.Add();
                    dataGridView3.Rows[n].Cells[0].Value = item["Username"].ToString();
                    dataGridView3.Rows[n].Cells[1].Value = item["CantidadFacturas"].ToString();
                }

                spVendedoresConMayorCantDeFacturas.Connection.Close();
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void btn_mayorMontoFacturas_Click(object sender, EventArgs e)
        {
            if (txt_anio.Text == "")
            {
                MessageBox.Show("Ingrese un año");
                return;
            }

            try
            {
                dataGridView4.Rows.Clear();
                int si = cb_trimestre.SelectedIndex;
                BaseDeDatos bd = new BaseDeDatos();
                var spVendedoresConMayorMontoFacturado = bd.obtenerStoredProcedure("vendedoresConMayorMontoFacturado");
                spVendedoresConMayorMontoFacturado.Parameters.Add("@anio", SqlDbType.Int).Value = Convert.ToInt32(txt_anio.Text);
                spVendedoresConMayorMontoFacturado.Parameters.Add("@trimestre", SqlDbType.Int).Value = Convert.ToInt32(cb_trimestre.Items[si]);

                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = spVendedoresConMayorMontoFacturado;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                foreach (DataRow item in dbdataset.Rows)
                {
                    int n = dataGridView4.Rows.Add();
                    dataGridView4.Rows[n].Cells[0].Value = item["Username"].ToString();
                    dataGridView4.Rows[n].Cells[1].Value = item["MontoTotal"].ToString();
                }

                spVendedoresConMayorMontoFacturado.Connection.Close();
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void txt_anio_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_anio_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if(!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
                MessageBox.Show("Debe ser un año");
            }
        }
    }
}
