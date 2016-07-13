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

namespace WindowsFormsApplication1.ABM_Rol
{
    public partial class AltaRol : FormMaestro
    {
        public AltaRol()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        protected override void interactuar()
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spCrearRol = bd.obtenerStoredProcedure("crearRol");
            spCrearRol.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txt_nombreRol.Text;
            spCrearRol.Parameters.Add("@Habilitado", SqlDbType.Bit).Value = cb_habilitado.CheckState;
            var reader = spCrearRol.ExecuteReader();
            reader.Read();
            int idrol = int.Parse(reader[0].ToString());

            try
            {
                foreach (DataGridViewRow item in dataGridView_funcionalidades.Rows)
                {
                    if (bool.Parse(item.Cells[0].Value.ToString()))
                    {
                        var spagregarFuncionalidadARol = bd.obtenerStoredProcedure("agregarFuncionalidadARol");
                        spagregarFuncionalidadARol.Parameters.Add("@Id_Funcionalidad", SqlDbType.VarChar).Value = item.Cells[1].Value.ToString();
                        spagregarFuncionalidadARol.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = idrol;
                        spagregarFuncionalidadARol.ExecuteNonQuery();
                        spagregarFuncionalidadARol.Connection.Close();
                    }
                }


                spCrearRol.Connection.Close();
                MessageBox.Show("Nuevo Rol cargado");
                this.Close();
            }

            catch (SqlException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            this.submitir();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AltaRol_Load(object sender, EventArgs e)
        {
            validador.textBoxsNoVacios(new List<TextBox>(new[] {
                txt_nombreRol
            }));

            this.cargarTabla();
        }

        private void cargarTabla()
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerFuncionalidades = bd.obtenerStoredProcedure("obtenerFuncionalidades");

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerFuncionalidades;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView_funcionalidades.Rows.Add();
                dataGridView_funcionalidades.Rows[n].Cells[0].Value = "false";
                dataGridView_funcionalidades.Rows[n].Cells[1].Value = item["Id_Funcionalidad"].ToString();
                dataGridView_funcionalidades.Rows[n].Cells[2].Value = item["Nombre_Funcionalidad"].ToString();
            }

        }

        private void txt_nombreRol_TextChanged(object sender, EventArgs e)
        {

        }

        private void cb_habilitado_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}