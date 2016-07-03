using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.ConexionBD;

namespace WindowsFormsApplication1.Generar_Publicación
{
    public partial class GenerarSubasta : Form
    {
        public GenerarSubasta()
        {
            InitializeComponent();
        }

        private void GenerarSubasta_Load(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            var spObtenerVisibilidades = bd.obtenerStoredProcedure("ObtenerVisibilidades");
            var reader = spObtenerVisibilidades.ExecuteReader();
            while (reader.Read())
            {
                string visibilidad = reader[1].ToString();
                comboBox1.Items.Add(visibilidad);
            }

            var spObtenerEstados = bd.obtenerStoredProcedure("obtenerEstadosElegibles");
            var reader2 = spObtenerEstados.ExecuteReader();
            while (reader2.Read())
            {
                string estado = reader2[1].ToString();
                comboBox2.Items.Add(estado);
            }
        }
    }
}
