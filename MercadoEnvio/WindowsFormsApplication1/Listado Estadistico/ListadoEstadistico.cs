using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    }
}
