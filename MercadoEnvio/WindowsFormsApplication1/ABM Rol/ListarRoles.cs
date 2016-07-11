using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using WindowsFormsApplication1.ConexionBD;

namespace WindowsFormsApplication1.ABM_Rol
{
    public partial class ListarRoles : Form
    {
        public ListarRoles()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.cargarTabla();
        }

        private void cargarTabla()
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerRoles = bd.obtenerStoredProcedure("obtenerRoles");

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerRoles;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Id_Rol"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Nombre_Rol"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Habilitado"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = "Dar de baja";
                dataGridView1.Rows[n].Cells[4].Value = "Modificar";

            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && (e.RowIndex != -1))
            {
                BaseDeDatos bd = new BaseDeDatos();
                var spBajaRol = bd.obtenerStoredProcedure("bajaRol");
                spBajaRol.Parameters.Add("@Id_Rol", SqlDbType.VarChar).Value = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                spBajaRol.ExecuteNonQuery();
                spBajaRol.Connection.Close();
                MessageBox.Show("El rol fue dado de baja");
                this.cargarTabla();
            }
            if (e.ColumnIndex == 4 && (e.RowIndex != -1))
            {
                ModificarRol mr = new ModificarRol(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                mr.Show();

            }

        }

    }
}
