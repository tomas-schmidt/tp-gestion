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

namespace WindowsFormsApplication1.Generar_Publicación
{
    public partial class ModificarSubasta : FormMaestro
    {
        private int idPublicacion;

        public ModificarSubasta(int idPublicacion)
        {
            this.idPublicacion = idPublicacion;
            InitializeComponent();
        }

        private void ModificarSubasta_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_montoInicial,
                txt_descripcion
            }));

            validador.textBoxsNumericos(new List<TextBox>(new[] {
                txt_montoInicial
            }));

            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerVisibilidades = bd.obtenerStoredProcedure("ObtenerVisibilidades");
            var reader = spObtenerVisibilidades.ExecuteReader();
            while (reader.Read())
            {
                string visibilidad = reader[1].ToString();
                comboBox1.Items.Add(visibilidad);
            }
            spObtenerVisibilidades.Connection.Close();

            var spObtenerEstados = bd.obtenerStoredProcedure("obtenerEstadosElegibles");
            var reader2 = spObtenerEstados.ExecuteReader();
            while (reader2.Read())
            {
                string estado = reader2[1].ToString();
                comboBox2.Items.Add(estado);
            }
            spObtenerEstados.Connection.Close();

            var spObtenerRubros = bd.obtenerStoredProcedure("obtenerRubros");

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerRubros;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = "false";
                dataGridView1.Rows[n].Cells[1].Value = item["Desc_Corta"].ToString();
            }
            spObtenerRubros.Connection.Close();

            var spObtenerPublicacion = bd.obtenerStoredProcedure("obtenerPublicacionAModificar");
            spObtenerPublicacion.Parameters.Add("@Id_Publicacion", SqlDbType.Int).Value = idPublicacion;

            sda.SelectCommand = spObtenerPublicacion;
            DataTable dbdataset2 = new DataTable();
            sda.Fill(dbdataset2);
            foreach (DataRow item2 in dbdataset2.Rows)
            {
                txt_descripcion.Text = item2["Descripcion"].ToString();
                txt_montoInicial.Text = (Convert.ToInt32(item2["Monto"])).ToString();
                comboBox1.SelectedItem = item2["Visibilidad_Desc"].ToString();
                comboBox2.SelectedItem = comboBox2.Items[0];
                checkBox1.Checked = (bool)item2["Preguntas"];
                checkBox2.Checked = (bool)item2["Envio"];
            }

            var spObtenerRubrosPublicacion = bd.obtenerStoredProcedure("obtenerRubrosPublicacion");
            spObtenerRubrosPublicacion.Parameters.Add("@Id_Publicacion", SqlDbType.VarChar).Value = idPublicacion;


            sda.SelectCommand = spObtenerRubrosPublicacion;
            DataTable dbdataset3 = new DataTable();
            sda.Fill(dbdataset3);
            foreach (DataRow item2 in dbdataset3.Rows)
            {

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Cells[1].Value.ToString() == item2[0].ToString())
                    {
                        item.Cells[0].Value = "true";
                    }
                }
            }
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btn_generarSubasta_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        protected override void interactuar()
        {
            bool poseeUnRubro = false;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (bool.Parse(item.Cells[0].Value.ToString()))
                {
                    poseeUnRubro = true;
                }
            }

            if (poseeUnRubro == false)
            {
                MessageBox.Show("Debe seleccionar como minimo un rubro");
                return;
            }



            try
            {
                int si1 = comboBox1.SelectedIndex;
                int si2 = comboBox2.SelectedIndex;
                BaseDeDatos bd = new BaseDeDatos();
                var spModificarPublicacion = bd.obtenerStoredProcedure("modificarSubasta");
                spModificarPublicacion.Parameters.Add("@Id_Publicacion", SqlDbType.Int).Value = idPublicacion;
                spModificarPublicacion.Parameters.Add("@Monto", SqlDbType.Float).Value = txt_montoInicial.Text;     
                spModificarPublicacion.Parameters.Add("@Preguntas", SqlDbType.Bit).Value = checkBox1.CheckState;
                spModificarPublicacion.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = txt_descripcion.Text;
                spModificarPublicacion.Parameters.Add("@Estado", SqlDbType.VarChar).Value = (string)comboBox2.Items[si2];
                spModificarPublicacion.Parameters.Add("@Envio", SqlDbType.Bit).Value = checkBox2.CheckState;
                var reader = spModificarPublicacion.ExecuteReader();
                reader.Read();

                var spEliminarRubrosDePublicacion = bd.obtenerStoredProcedure("eliminarRubrosDePublicacion");
                spEliminarRubrosDePublicacion.Parameters.Add("@Id_Publicacion", SqlDbType.Int).Value = idPublicacion;
                spEliminarRubrosDePublicacion.ExecuteNonQuery();

                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (bool.Parse(item.Cells[0].Value.ToString()))
                    {
                        var spAgregarRubroAPublicacion = bd.obtenerStoredProcedure("agregarRubroAPublicacion");
                        spAgregarRubroAPublicacion.Parameters.Add("@Rubro", SqlDbType.VarChar).Value = item.Cells[1].Value.ToString();
                        spAgregarRubroAPublicacion.Parameters.Add("@Id_Publicacion", SqlDbType.Int).Value = idPublicacion;
                        spAgregarRubroAPublicacion.ExecuteNonQuery();
                        spAgregarRubroAPublicacion.Connection.Close();
                    }
                }
                spModificarPublicacion.Connection.Close();
                MessageBox.Show("Cambios guardados con exito");
                this.Close();
            }
            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_descripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int si1 = comboBox1.SelectedIndex;
            if ((string)comboBox1.Items[si1] == "Gratis")
            {
                checkBox2.Checked = false;
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txt_montoInicial_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
