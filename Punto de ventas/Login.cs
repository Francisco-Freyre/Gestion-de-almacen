using Punto_de_ventas.models;
using Punto_de_ventas.modelsclass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas
{
    public partial class Login : Form
    {
        private string fecha = DateTime.Now.ToString("dd/MMM/yyy");
        private Usuario usuario = new Usuario();
        public static Caja Caja = new Caja();
        public Login()
        {
            InitializeComponent();
            label_Mensaje.Text = "";
            textBox_Usuario.Focus();
        }

        private void textBox_Usuario_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Usuario.Text == "")
            {
                label_Usuario.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label_Usuario.Text = "Usuario";
                label_Usuario.ForeColor = Color.Green;
            }
            label_Mensaje.Text = "";
        }

        private void textBox_Contraseña_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Contraseña.Text == "")
            {
                label_Contraseña.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label_Contraseña.Text = "Contraseña";
                label_Contraseña.ForeColor = Color.Green;
            }
            label_Mensaje.Text = "";
        }
        private void iniciar()
        {
            if (textBox_Usuario.Text == "")
            {
                label_Usuario.Text = "Ingrese Usuario";
                label_Usuario.ForeColor = Color.Red;
                textBox_Usuario.Focus(); 
            }
            else
            {
                if (textBox_Contraseña.Text == "")
                {
                    label_Contraseña.Text = "Ingrese Contraseña";
                    label_Contraseña.ForeColor = Color.Red;
                    textBox_Contraseña.Focus();

                }
                else
                {
                    object[] objtes = usuario.login(textBox_Usuario.Text, textBox_Contraseña.Text);
                    List<usuarios> listUsuario = (List<usuarios>)objtes[0];
                    List<Cajas> listCaja = (List<Cajas>)objtes[1];

                    if (0 < listUsuario.Count)
                    {
                        if ("Admin" == listUsuario[0].Rol)
                        {
                            Form1 form1 = new Form1(listUsuario, listCaja);
                            form1.Show();
                            Visible = false;
                        }
                        else
                        {
                            if (0 < listCaja.Count)
                            {
                                Form1 form1 = new Form1(listUsuario, listCaja);
                                form1.Show();
                                Visible = false;
                            }
                            else
                            {
                                label_Mensaje.Text = "No hay cajas disponibles";
                            }
                        }
                    }
                    else
                    {
                        label_Mensaje.Text = "Usuario o contraseña incorrecta";
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iniciar();
            //String has = Encriptar.EncryptData("123", "FOKO");
            //textBox_Usuario.Text = Encriptar.DecryptData(has, "FOKO");
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void textBox_Contraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                e.Handled = true;
                iniciar();
            }
        }

        private void textBox_Usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox_Usuario.Focus())
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    e.Handled = true;
                    textBox_Contraseña.Focus();
                }
            }
        }

        private void textBox_Usuario_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
