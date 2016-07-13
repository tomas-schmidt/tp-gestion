using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Validador
    {
        List<TextBox> textBoxNoVacios = new List<TextBox>();
        List<TextBox> textBoxNumericos = new List<TextBox>();
        List<TextBox> textBoxDecimales = new List<TextBox>();

        public void textBoxsNoVacios(List<TextBox> listaTxtBox)
        {
            textBoxNoVacios = listaTxtBox;
        }

        public void textBoxsNumericos(List<TextBox> listaTxtBox)
        {
            textBoxNumericos = listaTxtBox;
        }

        public void textBoxsDecimales(List<TextBox> listaTxtBox)
        {
            textBoxDecimales = listaTxtBox;
        }


        public void validar()
        {
            string mensajeError = validarTextBoxNoVacios();
            mensajeError += validarTextBoxNumericos();
            mensajeError += validarTextBoxDecimales();

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
                if (!regexNumerica.IsMatch(txtbox.Text))
                {
                    mensajeError += "El campo " + txtbox.Name + " debe ser numerico mayor a 0\n";
                }
            }

            return mensajeError;
        }

        private string validarTextBoxDecimales()
        {
            string mensajeError = "";
            try
            {
                foreach (TextBox txtbox in textBoxDecimales)
                {

                    Convert.ToDouble(txtbox.Text);

                }
            }
            catch (System.FormatException)
            {
                mensajeError += "El campo " /*+ txtbox.Name*/ + " debe ser numerico\n";
            }
            return mensajeError;

        }
    }
}