using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.ABM_Rol;
using WindowsFormsApplication1.ABM_Usuario;
using WindowsFormsApplication1.ABM_Visibilidad;
using WindowsFormsApplication1.Calificar;
using WindowsFormsApplication1.ComprarOfertar;
using WindowsFormsApplication1.ConexionBD;
using WindowsFormsApplication1.Facturas;
using WindowsFormsApplication1.Generar_Publicación;
using WindowsFormsApplication1.Historial_Cliente;

namespace WindowsFormsApplication1
{
    public partial class ElegirFuncionalidad : Form
    {
        private int idrol;
        private int idUser;

        public ElegirFuncionalidad(int idrol, int idUser)
        {
            InitializeComponent();
            this.idrol = idrol;
            this.idUser = idUser;
        }

        private void ElegirFuncionalidad_Load(object sender, EventArgs e)
        {
            this.cargarTabla();
        }
        private void cargarTabla()
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerFuncionalidadesDeRol = bd.obtenerStoredProcedure("ObtenerFuncionalidadesDeRol");
            spObtenerFuncionalidadesDeRol.Parameters.Add("@Id_Rol", SqlDbType.VarChar).Value = idrol;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerFuncionalidadesDeRol;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView_funcionalidades.Rows.Add();
                dataGridView_funcionalidades.Rows[n].Cells[0].Value = item["Nombre_Funcionalidad"].ToString();
                dataGridView_funcionalidades.Rows[n].Cells[1].Value = "Seleccionar";
               
            }

        }

        private void dataGridView_funcionalidades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (dataGridView_funcionalidades.Rows[e.RowIndex].Cells[0].Value.ToString() == "ABMRol")
                {
                    ABMRol abmrol = new ABMRol();
                    abmrol.Show();
                }
                if (dataGridView_funcionalidades.Rows[e.RowIndex].Cells[0].Value.ToString() == "ABMUsuario")
                {
                    ABMUsuario abmusuario = new ABMUsuario();
                    abmusuario.Show();
                }
                if (dataGridView_funcionalidades.Rows[e.RowIndex].Cells[0].Value.ToString() == "ABMVisibilidad")
                {
                    ABMVisibilidad abmVisibilidad = new ABMVisibilidad();
                    abmVisibilidad.Show();
                }
                if (dataGridView_funcionalidades.Rows[e.RowIndex].Cells[0].Value.ToString() == "GenerarPublicacion")
                {
                    GenerarPublicacion gp = new GenerarPublicacion(idUser);
                    gp.Show();
                }

                if (dataGridView_funcionalidades.Rows[e.RowIndex].Cells[0].Value.ToString() == "Comprar/Ofertar")
                {
                    ListarPublicaciones lp = new ListarPublicaciones(idUser);
                    lp.Show();
                }

                if (dataGridView_funcionalidades.Rows[e.RowIndex].Cells[0].Value.ToString() == "Calificar")
                {
                    ListarComprasSinCalificar lc = new ListarComprasSinCalificar(idUser);
                    lc.Show();
                }

                if (dataGridView_funcionalidades.Rows[e.RowIndex].Cells[0].Value.ToString() == "ConsultarFacturas")
                {
                    ListarFacturas lf = new ListarFacturas(idUser);
                    lf.Show();
                }

                if (dataGridView_funcionalidades.Rows[e.RowIndex].Cells[0].Value.ToString() == "ObtenerHistorial")
                {
                    HistorialCliente hc = new HistorialCliente(idUser);
                    hc.Show();
                }
            }
              
        
        }

    }
}
