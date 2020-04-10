using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas.modelsclass
{
    public class TextBoxEvent
    {
        public void textKeyPress(KeyPressEventArgs e)
        {
            //Solo ingresar texto
            if (char.IsLetter(e.KeyChar)) { e.Handled = false; }
            //tecla borrar
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
            //tecla espacio
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
            else { e.Handled = true; }
        }

        public void numberKeyPress(KeyPressEventArgs e)
        {
            //Solo ingresar numeros
            if (char.IsDigit(e.KeyChar)) { e.Handled = false; }

            else if (char.IsLetter(e.KeyChar)) { e.Handled = true; }

            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }

            else if (char.IsSeparator(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = true; }
        }

        public void numberDecimalKeyPress(TextBox textBox, KeyPressEventArgs e)
        {
            //Solo ingresar numeros
            if (char.IsDigit(e.KeyChar)) { e.Handled = false; }
            // borrar
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
            //verificar si hay punto decimal
            else if ((e.KeyChar == '.') && (!textBox.Text.Contains("."))) { e.Handled = false; }
            else { e.Handled = true; }
            
        }
        public bool comprobarFormatoCorreo(string email)
        {
            if (new EmailAddressAttribute().IsValid(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
