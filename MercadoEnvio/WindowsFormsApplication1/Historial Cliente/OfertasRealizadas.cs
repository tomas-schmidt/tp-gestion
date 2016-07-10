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

namespace WindowsFormsApplication1.Historial_Cliente
{
    public partial class OfertasRealizadas : Form
    {
        private int idUserActual;

        public OfertasRealizadas(int idUserActual)
        {
            InitializeComponent();
            this.idUserActual = idUserActual;
        }

        private void OfertasRealizadas_Load(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerOfertas = bd.obtenerStoredProcedure("obtenerOfertas");
            spObtenerOfertas.Parameters.Add("@Id_User", SqlDbType.Int).Value = idUserActual;

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = spObtenerOfertas;
            DataTable dbdataset = new DataTable();
            sda.Fill(dbdataset);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dbdataset.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["Descripcion"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["Monto_Ofertado"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item["Fecha"].ToString();
            }
        }
    }
}
