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
        private int idUser;

        public GenerarPublicacion(int idUser)
        {
            InitializeComponent();
            this.idUser = idUser;
        }

        private void btn_generarCompra_Click(object sender, EventArgs e)
        {
            GenerarCompra gp = new GenerarCompra(idUser);
            gp.Show();
        }

        private void btn_generarSubasta_Click(object sender, EventArgs e)
        {
            GenerarSubasta gs = new GenerarSubasta(idUser);
            gs.Show();
        }

        private void GenerarPublicacion_Load(object sender, EventArgs e)
        {

        }

        private void btn_borradores_Click(object sender, EventArgs e)
        {
            ListarBorradores lb = new ListarBorradores(idUser);
            lb.Show();
        }
    }
}
