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
    public partial class Salidas : Form
    {
        private string codigo = "", tipo, usuariocorte, pagocon, sucambio;
        private int cantidad = 0, idVenta = 0, ticket = 0, idusuario = 0;
        GroupBox groupBox;
        DateTimePicker dateTimePicker;
        private int idUsuario, caja;
        private Label label;
        private string dia = DateTime.Now.ToString("dd");
        private string mes = DateTime.Now.ToString("MMM");
        private string año = DateTime.Now.ToString("yyy");
        private string fecha = DateTime.Now.ToString("dd/MMM/yyy");
        public static Caja Caja = new Caja();

        public Salidas(int idUsuario, int caja)
        {
            InitializeComponent();

            this.idUsuario = idUsuario;
            this.caja = caja;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            Visible = false;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ClassModels.imprimir.printDocument(e, groupBox, caja, idUsuario, tipo, dateTimePicker, usuariocorte, pagocon, sucambio);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ingresar un monto.", "Punto Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Ingresar un motivo.", "Punto Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ClassModels.Caja.salidasIngresos(idUsuario, caja, fecha, textBox1.Text, textBox2.Text);
                    Visible = false;
                }
                idUsuario = 0;
                caja = 0;
                groupBox = null;
                tipo = "Imprimir";
                dateTimePicker = null;
                printDocument1.Print();
            }
        }
    }
}
