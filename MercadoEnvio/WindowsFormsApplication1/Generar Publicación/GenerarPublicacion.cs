using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Generar_Publicación
{
    public partial class GenerarPublicacion : Form
    {
        public GenerarPublicacion()
        {
            InitializeComponent();
        }

        private void btn_generarCompra_Click(object sender, EventArgs e)
        {
            GenerarCompra gp = new GenerarCompra();
            gp.Show();
        }

        private void btn_generarSubasta_Click(object sender, EventArgs e)
        {
            GenerarSubasta gs = new GenerarSubasta();
            gs.Show();
        }
    }
}
