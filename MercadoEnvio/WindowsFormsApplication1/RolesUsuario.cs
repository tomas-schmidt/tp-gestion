﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.ConexionBD;

namespace WindowsFormsApplication1
{
    public partial class RolesUsuario : Form
    {
        private int iduser;

        public RolesUsuario(int idusuario)
        {
            InitializeComponent();
            this.iduser = idusuario;
        }

        
        private void cargarTabla()
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerRolesDeUsuario = bd.obtenerStoredProcedure("ObtenerRolesDeUsuario");
            spObtenerRolesDeUsuario.Parameters.Add("@Id_User", SqlDbType.VarChar).Value = iduser;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerRolesDeUsuario;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView_roles.Rows.Add();
                dataGridView_roles.Rows[n].Cells[0].Value = "Acceder Como:";
                dataGridView_roles.Rows[n].Cells[1].Value = item["Nombre_Rol"].ToString();
                dataGridView_roles.Rows[n].Cells[2].Value = item[0].ToString(); //idrol
            }

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FuncionalidadesUsuario_Load(object sender, EventArgs e)
        {
            this.cargarTabla();
        }

        private void dataGridView_roles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && (e.RowIndex != -1))
            {
                ElegirFuncionalidad ef = new ElegirFuncionalidad(Convert.ToInt32(dataGridView_roles.Rows[e.RowIndex].Cells[2].Value.ToString()), this.iduser);
                ef.Show();

                
                /*BaseDeDatos bd = new BaseDeDatos();
                var spObtenerRolesDeUsuario = bd.obtenerStoredProcedure("ObtenerRolesDeUsuario");
                spObtenerRolesDeUsuario.Parameters.Add("@Id_User", SqlDbType.VarChar).Value = iduser;
                var reader = spObtenerRolesDeUsuario.ExecuteReader();
                //for(int i=0;i<=e.RowIndex;i++)
                reader.Read();
                ElegirFuncionalidad ef = new ElegirFuncionalidad(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[3]));
                ef.Show();*/
            }
        }
    }
}
