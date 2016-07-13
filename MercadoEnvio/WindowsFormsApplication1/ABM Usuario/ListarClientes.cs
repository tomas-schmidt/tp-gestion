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
    public partial class ListarClientes : FormMaestro
    {
        public ListarClientes()
        {
            InitializeComponent();
        }

        private void txt_nombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void ListarClientes_Load(object sender, EventArgs e)
        {
            validador.textBoxsNumericos(new List<TextBox>(new[] {
                txt_dni
            }));
        }

        protected override void interactuar()
        {
            dataGridView1.Rows.Clear();

            string consulta;
            consulta = "select c.*, Habilitado, Doc_Desc from C_HASHTAG.Cliente c JOIN C_HASHTAG.Usuario u ON (u.Id_User = c.Id_User) join C_HASHTAG.Tipo_Doc td on (td.Doc_Codigo = c.Tipo_Doc) where Id_Cliente is not null";

            if (txt_dni.Text != "")
            {
                consulta = consulta + " and Nro_doc = '" + txt_dni.Text + "'";
            }
            if (txt_nombre.Text != "")
            {
                consulta = consulta + " and Nombre like '%" + txt_nombre.Text + "%'";
            }
            if (txt_apellido.Text != "")
            {
                consulta = consulta + " and Apellido like '%" + txt_apellido.Text + "%'";
            }
            if (txt_Email.Text != "")
            {
                consulta = consulta + " and Mail like '%" + txt_Email.Text + "%'";
            }


            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerClientes = bd.obtenerConsulta(consulta);

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerClientes;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Id_Cliente"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Nombre"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Apellido"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Nro_Doc"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item["Mail"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Doc_Desc"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = item["Habilitado"].ToString();
                dataGridView1.Rows[n].Cells[7].Value = "Baja/Alta";
                dataGridView1.Rows[n].Cells[8].Value = "Modificar";

            }
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7 && (e.RowIndex != -1))
            {
                BaseDeDatos bd = new BaseDeDatos();
                var spcambiarEstadoEmpresa = bd.obtenerStoredProcedure("cambiarEstadoCliente");
                spcambiarEstadoEmpresa.Parameters.Add("@Id_Cliente", SqlDbType.VarChar).Value = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                spcambiarEstadoEmpresa.ExecuteNonQuery();
                spcambiarEstadoEmpresa.Connection.Close();
                MessageBox.Show("El cliente fue dado de baja/alta");
            }
            if (e.ColumnIndex == 8 && (e.RowIndex != -1))
            {
                ModificarCliente mc = new ModificarCliente(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                mc.Show();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            txt_apellido.Clear();
            txt_dni.Clear();
            txt_Email.Clear();
            txt_nombre.Clear();

        }
    }
}
