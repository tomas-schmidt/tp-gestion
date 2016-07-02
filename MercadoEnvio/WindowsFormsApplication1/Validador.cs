using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Validador

    {
        List<TextBox> textBoxNoVacios;
        List<TextBox> textBoxNumericos;
        
        public void textBoxsNoVacios(List<TextBox> listaTxtBox)
        {
            textBoxNoVacios = listaTxtBox;
        }

        public void textBoxsNumericos(List<TextBox> listaTxtBox)
        {
            textBoxNumericos = listaTxtBox;
        }


        public void validar()
        {
            string mensajeError = validarTextBoxNoVacios();
            mensajeError += validarTextBoxNumericos();

            if (mensajeError != "")
            {
                throw new ValidacionException(mensajeError);
            }
        }
        

        private string validarTextBoxNoVacios()
        {
            string mensajeError = "";
            foreach (TextBox txtbox in textBoxNoVacios)
            {
                if (txtbox.Text == "")
                {
                    mensajeError += "El campo " + txtbox.Name + " no puede estar vacio\n";
                }
            }
            return mensajeError;
        }

        private string validarTextBoxNumericos()
        {
            Regex regexNumerica = new Regex("^[0-9]*$");
            string mensajeError = "";
            foreach (TextBox txtbox in textBoxNumericos)
            {
                if (! regexNumerica.IsMatch(txtbox.Text))
                {
                    mensajeError += "El campo " + txtbox.Name + " debe ser numerico\n";
                }
            }

            return mensajeError;
        }
    
    }
}
