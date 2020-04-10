using Punto_de_ventas.Connection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas.modelsclass
{
    public class ReportesDeMovimientos : Conexion
    {
        public void buscarReportes(DateTimePicker dateTimePicker, DataGridView dataGridView)
        {
            var fecha_inicio = dateTimePicker.Value.Date.ToString("dd/MMM/yyy");
            dataGridView.DataSource = ReportesMoviminetos.Where(r => r.Fecha.Equals(fecha_inicio)).ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[2].DefaultCellStyle.ForeColor = Color.Green;
        }

        public void buscarMov(DateTimePicker dateTimePicker, DataGridView dataGridView, string tipo)
        {
            var fecha_inicio = dateTimePicker.Value.Date.ToString("dd/MMM/yyy");
            dataGridView.DataSource = Movimientos.Where(m => m.Fecha.Equals(fecha_inicio) && m.TipoMovimiento.Equals(tipo)).ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[2].DefaultCellStyle.ForeColor = Color.Green;
        }

        public void buscarMovSal(DateTimePicker dateTimePicker, DataGridView dataGridView)
        {
            var fecha_inicio = dateTimePicker.Value.Date.ToString("dd/MMM/yyy");
            dataGridView.DataSource = salidasdinero.Where(m => m.Fecha.Equals(fecha_inicio)).ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[3].Visible = false;
        }
    }
}
