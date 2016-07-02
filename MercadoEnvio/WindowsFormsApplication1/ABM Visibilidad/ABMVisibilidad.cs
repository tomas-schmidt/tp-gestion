using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1.ABM_Visibilidad
{
    public partial class ABMVisibilidad : Form
    {
        public ABMVisibilidad()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_altaVisibilidad_Click(object sender, EventArgs e)
        {
            AltaVisibilidad av = new AltaVisibilidad();
            av.Show();
        }
    }
}
