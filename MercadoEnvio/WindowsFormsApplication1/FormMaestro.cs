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

        public void RemoveDuplicate(DataGridView grv)
        {
            for (int currentRow = 0; currentRow < grv.Rows.Count - 1; currentRow++)
            {
                DataGridViewRow rowToCompare = grv.Rows[currentRow];

                for (int otherRow = currentRow + 1; otherRow < grv.Rows.Count; otherRow++)
                {
                    DataGridViewRow row = grv.Rows[otherRow];

                    bool duplicateRow = true;

                    for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                    {
                        if (!rowToCompare.Cells[cellIndex].Value.Equals(row.Cells[cellIndex].Value))
                        {
                            duplicateRow = false;
                            break;
                        }
                    }

                    if (duplicateRow)
                    {
                        grv.Rows.Remove(row);
                        otherRow--;
                    }
                }
            }
        }

    }
}
