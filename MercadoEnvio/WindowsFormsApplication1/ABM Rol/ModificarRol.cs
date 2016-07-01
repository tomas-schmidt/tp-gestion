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
    public partial class ModificarRol : Form
    {
        private int idamodificar;

        public ModificarRol(string idrol)
        {
            InitializeComponent();
            idamodificar = Convert.ToInt32(idrol);
        }

        private void ModificarRol_Load(object sender, EventArgs e)
        {
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


        private void btn_guardar_Click(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spactualizarYBorrarFuncionalidadesRol = bd.obtenerStoredProcedure("actualizarYBorrarFuncionalidadesRol");
            spactualizarYBorrarFuncionalidadesRol.Parameters.Add("@Id_Rol", SqlDbType.VarChar).Value = idamodificar;
            spactualizarYBorrarFuncionalidadesRol.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = txt_nombreRol.Text;
            spactualizarYBorrarFuncionalidadesRol.Parameters.Add("@Habilitado", SqlDbType.Bit).Value = cb_habilitado.CheckState;
            var reader = spactualizarYBorrarFuncionalidadesRol.ExecuteReader();
            reader.Read();

            try
            {
                foreach (DataGridViewRow item in dataGridView_funcionalidades.Rows)
                {
                    if (bool.Parse(item.Cells[0].Value.ToString()))
                    {
                        var spagregarFuncionalidadARol = bd.obtenerStoredProcedure("agregarFuncionalidadARol");
                        spagregarFuncionalidadARol.Parameters.Add("@Id_Funcionalidad", SqlDbType.VarChar).Value = item.Cells[1].Value.ToString();
                        spagregarFuncionalidadARol.Parameters.Add("@Id_Rol", SqlDbType.Int).Value = idamodificar;
                        spagregarFuncionalidadARol.ExecuteNonQuery();
                        spagregarFuncionalidadARol.Connection.Close();
                    }
                }


                spactualizarYBorrarFuncionalidadesRol.Connection.Close();
                MessageBox.Show("Rol modificado");
                txt_nombreRol.Clear();
            }

            catch (SqlException excepcion)
            {
                MessageBox.Show("Hubo un error en la base: " + excepcion.Message);
            }
        
        }

        private void dataGridView_funcionalidades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
