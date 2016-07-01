using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ABM_Usuario
{
    public partial class ABMUsuario : Form
    {
        public ABMUsuario()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_crearCliente_Click(object sender, EventArgs e)
        {
            CrearCliente crearCli = new CrearCliente();
            crearCli.Show();
        }

        private void btn_crearEmpresa_Click(object sender, EventArgs e)
        {
            CrearEmpresa crearEmpresa = new CrearEmpresa();
            crearEmpresa.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListarEmpresas listarEmpresas = new ListarEmpresas();
            listarEmpresas.Show();
        }
    }
}
