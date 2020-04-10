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
    public partial class TicketsVentas : Form
    {
        private string codigo = "", fecha, tipo, usuariocorte, pagocon, sucambio;
        private int cantidad = 0, idVenta = 0, ticket = 0, idusuario = 0, caja = 0, idUsuario;
        GroupBox groupBox;
        DateTimePicker dateTimePicker;
        public TicketsVentas()
        {
            InitializeComponent();
            comboBox1.DataSource = ClassModels.usuario.getUsuarios();
            comboBox1.ValueMember = "IdUsuario";
            comboBox1.DisplayMember = "Usuario";
            ClassModels.usuario.listaTickets(dataGridView1, dateTimePicker1, 1, textBox1.Text);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string dato;
            int datos2;
            dato = comboBox1.Text;
            datos2 = ClassModels.usuario.IdUsuarioResult(dato);
            ClassModels.usuario.listaTickets(dataGridView1, dateTimePicker1, datos2, textBox1.Text);
        }

        private void buttonImprimir_ticket_Click(object sender, EventArgs e)
        {
            idUsuario = Convert.ToInt16(labelNumeroTicket.Text);
            caja = Convert.ToInt16(labelNumeroTicket.Text);
            groupBox = null;
            tipo = "ticket";
            dateTimePicker = null;
            printDocument1.Print();
            idUsuario = 0;
            caja = 0;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string dato;
            //int datos2;
            //dato = comboBox1.Text;
            //datos2 = ClassModels.usuario.IdUsuarioResult(dato);
            //ClassModels.usuario.listaTickets(dataGridView1, dateTimePicker1, datos2, textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string dato;
            int datos2;
            dato = comboBox1.Text;
            datos2 = ClassModels.usuario.IdUsuarioResult(dato);
            ClassModels.usuario.listaTickets(dataGridView1, dateTimePicker1, datos2, textBox1.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                fecha = Convert.ToString(dataGridView1.CurrentRow.Cells[9].Value);
                caja = Convert.ToInt16(dataGridView1.CurrentRow.Cells[10].Value);
                idusuario = Convert.ToInt16(dataGridView1.CurrentRow.Cells[11].Value);
                ticket = Convert.ToInt32(dataGridView1.CurrentRow.Cells[12].Value);
                ClassModels.usuario.ventaXTicket(dataGridView2, fecha, caja, idusuario, ticket);
                labelNumeroTicket.Text = ticket.ToString();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            ClassModels.imprimir.printDocument(e, groupBox, caja, idUsuario, tipo, dateTimePicker, usuariocorte, pagocon, sucambio);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassModels.evento.numberKeyPress(e);
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows.Count != 0)
            {
                codigo = Convert.ToString(dataGridView2.CurrentRow.Cells[1].Value);
                cantidad = Convert.ToInt16(dataGridView2.CurrentRow.Cells[4].Value);
                idVenta = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
            }

            textBoxCantidad_Eliminar.Text = Convert.ToString(cantidad);
        }

        private void buttonEliminar_Completo_Click(object sender, EventArgs e)
        {
            ClassModels.usuario.borrarVentaCompleta(fecha, idusuario, ticket);
            ClassModels.usuario.ventaXTicket(dataGridView2, fecha, caja, idusuario, ticket);
        }

        private void buttonEliminar_Cantidad_Click(object sender, EventArgs e)
        {
            ClassModels.usuario.borrarVenta(Convert.ToInt16(textBoxCantidad_Eliminar.Text), idVenta, codigo);
        }
    }
}
