using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.Calificar;

namespace WindowsFormsApplication1.Historial_Cliente
{
    public partial class HistorialCliente : Form
    {
        private int idUserActual;

        public HistorialCliente(int idUserActual)
        {
            this.idUserActual = idUserActual;
            InitializeComponent();
        }

        private void HistorialCliente_Load(object sender, EventArgs e)
        {

        }

        private void btn_compras_Click(object sender, EventArgs e)
        {
            ComprasConcretadas cc = new ComprasConcretadas(idUserActual);
            cc.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HistorialCalificaciones hc = new HistorialCalificaciones(idUserActual);
            hc.Show();
        }

        private void btn_ofertas_Click(object sender, EventArgs e)
        {
            OfertasRealizadas or = new OfertasRealizadas(idUserActual);
            or.Show();
        }
    }
}
