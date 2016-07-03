﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial  class FormMaestro : Form
    {
        protected Validador validador = new Validador();

        public FormMaestro()
        {
            InitializeComponent();
        }

        protected void submitir()
        {
            try
            {
                validador.validar();
                this.interactuar();
            }
            catch(ValidacionException ve)
            {
                MessageBox.Show(ve.Message);
            }


        }

        protected virtual void interactuar() { }

        private void FormMaestro_Load(object sender, EventArgs e)
        {

        }

    }
}
