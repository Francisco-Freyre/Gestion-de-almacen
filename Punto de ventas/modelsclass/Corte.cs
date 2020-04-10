using Punto_de_ventas.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas.modelsclass
{
    public class Corte : Conexion
    {
        internal void cortes(Label label, Label label2,Label label3, Label label4, Label label5, Label label6,
            Label label7, Label label8, Label label9, Label label10, Label label11, Label label12, Label label13,
            Label label14, Label label15, Label label16, Label label17, DateTimePicker dateTimePicker , int idUsuario, string usuario)
        {
            int cant= 0;
            decimal cost = 0;
            var fecha_inicio = dateTimePicker.Value.Date.ToString("dd/MMM/yyy");
            label10.Text = fecha_inicio;
            Decimal importe, ganancia = 0, total = 0, totalsalidas= 0, totalcaja = 0;
            importe = 0;
            var inicio = entradasIniciales.Where(e => e.Fecha.Equals(fecha_inicio) && e.IdUsuario.Equals(idUsuario)).ToList();
            var abono2 = abonos.Where(a => a.Usuario.Equals(usuario) && a.Fecha.Equals(fecha_inicio)).ToList();
            if (inicio.Count > 0)
            {
                label5.Text = inicio[0].Ingreso;
                label6.Text = inicio[0].Ingreso;
                label7.Text = inicio[0].Ingreso;
                label11.Text = inicio[0].Ingreso;
            }
            else
            {
                label5.Text = "$0.00";
                label6.Text = "$0.00";
                label7.Text = "$0.00";
            }
            var venta = Ventas.Where(t => t.IdUsuario.Equals(idUsuario) && t.Fecha.Equals(fecha_inicio)).ToList();
            if (venta.Count > 0)
            {
                venta.ForEach(item => {
                    total = 0;
                    total = (Convert.ToDecimal(item.Importe.Replace("$", ""))) - (Convert.ToDecimal(item.Costo) * item.Cantidad);
                    ganancia = ganancia + total;
                    importe += Convert.ToDecimal(item.Importe.Replace("$", ""));
                    cant += item.Cantidad;
                    cost += Convert.ToDecimal(item.Costo);
                });
                label.Text = String.Format("${0:#,###,###,##0.00####}", importe);
                label2.Text = String.Format("${0:#,###,###,##0.00####}", importe);
                if (abono2.Count != 0)
                {
                    label3.Text = String.Format("${0:#,###,###,##0.00####}", importe + Convert.ToDecimal(abono2[0].Importe));
                }
                else
                {
                    label3.Text = String.Format("${0:#,###,###,##0.00####}", importe);
                }
                label4.Text = String.Format("${0:#,###,###,##0.00####}", ganancia);
                label8.Text = String.Format("${0:#,###,###,##0.00####}", importe);
                label12.Text = String.Format("${0:#,###,###,##0.00####}", importe);
                label14.Text = String.Format("${0:#,###,###,##0.00####}", ganancia);
            }
            else
            {
                label.Text = "$0.00";
            }
            var salidas = salidasdinero.Where(s => s.Fecha.Equals(fecha_inicio) && s.IdUsuario.Equals(idUsuario)).ToList();
            if (salidas.Count != 0)
            {
                salidas.ForEach(item =>
                {
                    totalsalidas += Convert.ToDecimal(item.Monto.Replace("$", ""));
                });
                label15.Text = String.Format("${0:#,###,###,##0.00####}", totalsalidas);
                label16.Text = String.Format("${0:#,###,###,##0.00####}", totalsalidas);
                label17.Text = String.Format("${0:#,###,###,##0.00####}", totalsalidas);
            }
            
            total = importe + Convert.ToDecimal(inicio[0].Ingreso.Replace("$", ""));
            label9.Text = String.Format("${0:#,###,###,##0.00####}", total);
            totalcaja = total - totalsalidas;
            label13.Text = String.Format("${0:#,###,###,##0.00####}", totalcaja );
        }

        internal void cortesGlobales(Label label, Label label2, Label label3, Label label4, Label label5, Label label6,
            Label label7, Label label8, Label label9, Label label10, Label label11, Label label12, Label label13,
            Label label14, Label label15, Label label16, Label label17, DateTimePicker dateTimePicker, int idUsuario)
        {
            int cant = 0;
            var fecha_inicio = dateTimePicker.Value.Date.ToString("dd/MMM/yyy");
            label10.Text = fecha_inicio;
            Decimal importe, ganancia = 0, total = 0, totalsalidas = 0, totalcaja = 0, cost = 0;
            importe = 0;
            var inicio = entradasIniciales.Where(e => e.Fecha.Equals(fecha_inicio)).ToList();
            var abono2 = abonos.Where(a => a.Fecha.Equals(fecha_inicio)).ToList();
            if (inicio.Count > 0)
            {
                label5.Text = inicio[0].Ingreso;
                label6.Text = inicio[0].Ingreso;
                label7.Text = inicio[0].Ingreso;
                label11.Text = inicio[0].Ingreso;
            }
            else
            {
                label5.Text = "$0.00";
                label6.Text = "$0.00";
                label7.Text = "$0.00";
            }
            var venta = Ventas.Where(t => t.Fecha.Equals(fecha_inicio)).ToList();
            if (venta.Count > 0)
            {
                venta.ForEach(item => {
                    total = Convert.ToDecimal(item.Importe.Replace("$", "")) - (Convert.ToDecimal(item.Costo) * item.Cantidad);
                    ganancia += total;
                    importe += Convert.ToDecimal(item.Importe.Replace("$", ""));
                    cant += item.Cantidad;
                    cost += Convert.ToDecimal(item.Costo);
                });
                label.Text = String.Format("${0:#,###,###,##0.00####}", importe);
                label2.Text = String.Format("${0:#,###,###,##0.00####}", importe);
                if (abono2.Count != 0)
                {
                    label3.Text = String.Format("${0:#,###,###,##0.00####}", importe + Convert.ToDecimal(abono2[0].Importe));
                }
                else
                {
                    label3.Text = String.Format("${0:#,###,###,##0.00####}", importe);
                }
                label4.Text = String.Format("${0:#,###,###,##0.00####}", ganancia);
                label8.Text = String.Format("${0:#,###,###,##0.00####}", importe);
                label12.Text = String.Format("${0:#,###,###,##0.00####}", importe);
                label14.Text = String.Format("${0:#,###,###,##0.00####}", ganancia);
            }
            else
            {
                label.Text = "$0.00";
            }
            var salidas = salidasdinero.Where(s => s.Fecha.Equals(fecha_inicio)).ToList();
            if (salidas.Count != 0)
            {
                salidas.ForEach(item =>
                {
                    totalsalidas += Convert.ToDecimal(item.Monto.Replace("$", ""));
                });
                label15.Text = String.Format("${0:#,###,###,##0.00####}", totalsalidas);
                label16.Text = String.Format("${0:#,###,###,##0.00####}", totalsalidas);
                label17.Text = String.Format("${0:#,###,###,##0.00####}", totalsalidas);
            }
            
            total = importe ;
            label9.Text = String.Format("${0:#,###,###,##0.00####}", total);
            totalcaja = total - totalsalidas;
            if (abono2.Count != 0)
            {
                label13.Text = String.Format("${0:#,###,###,##0.00####}", totalcaja + abono2[0].Importe);
            }
            else
            {
                label13.Text = String.Format("${0:#,###,###,##0.00####}", totalcaja);
            }
            
        }
    }
}
