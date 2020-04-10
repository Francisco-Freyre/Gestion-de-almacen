using LinqToDB;
using Punto_de_ventas.Connection;
using Punto_de_ventas.models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas.modelsclass
{
    public class Venta: Conexion
    {
        private Decimal importe = 0, totalPagar = 0m, ingresos, ingresosTotales;
        private string dia = DateTime.Now.ToString("dd");
        private string mes = DateTime.Now.ToString("MMM");
        private string año = DateTime.Now.ToString("yyy");
        private string fecha = DateTime.Now.ToString("dd/MMM/yyy");
        private bool verificar = false, suCambio = false;
        private int caja, idUsuario;

        public void start(int caja, int idUsuario)
        {
            this.caja = caja;
            this.idUsuario = idUsuario;
        }
        public List<Articulos> buscarProductos(string codigo)
        {
            return Producto.Where(p => p.Codigo.Equals(codigo)).ToList();
        }

        public void guardarVentasTempo(string codigo, int funcion, int caja, int idUsuario)
        {
            string importe, precios;
            int cantidad = 1, existencia;
            Decimal precio, importes;

            var ventaTempo = TempoVentas.Where(t => t.Codigo.Equals(codigo)
            && t.IdUsuario.Equals(idUsuario)).ToList();
            var product = Producto.Where(p => p.Codigo.Equals(codigo)).ToList();
            precio = Convert.ToDecimal(product[0].Precio);
            precios = String.Format("${0:#,###,###,##0.00####}", precio);
            if (ventaTempo.Count() > 0)
            {
                cantidad = ventaTempo[0].Cantidad;
                if(funcion == 0)
                {
                    cantidad += 1;
                }
                else
                {
                    cantidad--;
                }
                importes = precio * cantidad;
                importe = String.Format("${0:#,###,###,##0.00####}", importes);
                TempoVentas.Where(t => t.IdTempo.Equals(ventaTempo[0].IdTempo))
                                .Set(t => t.Cantidad, cantidad)
                                .Set(t => t.IdUsuario, idUsuario)
                                .Update();
            }
            else
            {
                TempoVentas.Value(t => t.Codigo, product[0].Codigo)
                            .Value(t => t.Descripcion, product[0].Descripcion)
                            .Value(t => t.Cantidad, 1)
                            .Value(t => t.IdUsuario, idUsuario)
                            .Insert();
            }
            existencia = product[0].Existencia;
            if (existencia == 0)
            {

            }
            else
            { 
                existencia--;
                Producto.Where(p => p.IdArticulo.Equals(product[0].IdArticulo))
                .Set(t => t.Existencia, existencia)
                .Update();
            }
                
        }

        public void buscarVentaTempo(DataGridView dataGridView, int num_pag, int reg_por_pag)
        {
            var query = TempoVentas.Where(t => t.IdUsuario.Equals(idUsuario)).ToList();
            int inicio = (num_pag - 1) * reg_por_pag;
            dataGridView.DataSource = query.Skip(inicio).Take(reg_por_pag).ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[4].Visible = false;
            dataGridView.Columns[3].DefaultCellStyle.ForeColor = Color.Green;
        }

        public void borrarVenta(string codigo, int cant, int caja, int idUsuario)
        {
            int cantidad = 0, existencia = 0;
            var ventatemp = TempoVentas.Where(t => t.Codigo.Equals(codigo)
            && t.IdUsuario.Equals(idUsuario)).ToList();
            cantidad = ventatemp[0].Cantidad;
            var producto = Producto.Where(p => p.Codigo.Equals(codigo)).ToList();
            existencia = producto[0].Existencia;

            if(cant == 1)
            {
                existencia += cantidad;
                TempoVentas.Where(t => t.IdTempo == ventatemp[0].IdTempo
                && t.IdUsuario.Equals(idUsuario)).Delete();
            }
            else
            {
                existencia++;
                guardarVentasTempo(codigo, 1, caja, idUsuario);
            }
            Producto.Where(p => p.IdArticulo.Equals(producto[0].IdArticulo))
                        .Set(t => t.Existencia, existencia)
                        .Update();
        }

        public void agregarVenta(string codigo, int cant, int caja, int idusuario)
        {
            int cantidad = 0, existencia = 0;
            var ventatemp = TempoVentas.Where(t => t.Codigo.Equals(codigo)
            && t.IdUsuario.Equals(idusuario)).ToList();
            cantidad = ventatemp[0].Cantidad;
            var producto = Producto.Where(p => p.Codigo.Equals(codigo)).ToList();
            existencia = producto[0].Existencia;

            if (existencia <= 0)
            {

            }
            else
            {
                guardarVentasTempo(codigo, 0, caja, idusuario);
            }
        }

        public void pagosCliente(TextBox textBox, Label label1, Label label2, Label label3, CheckBox checkBox)
        {
            Decimal pago, pagar;
            if (textBox.Text == "")
            {
                label1.Text = "Su cambio";
                label2.Text = "$0.00";
            }
            else
            {
                pagar = importe;
                pago = Convert.ToDecimal(textBox.Text);
                if (pago >= pagar)
                {
                    totalPagar = pago - pagar; 
                    if (totalPagar > ingresosTotales)
                    {
                        label1.Text = "No hay ingresos en caja";
                        label1.ForeColor = Color.Red;
                        verificar = false;
                        suCambio = false;
                    }
                    else
                    {
                        if (checkBox.Checked == true)
                        {
                            label1.Text = "Desmarque la opcion credito";
                            label1.ForeColor = Color.Red;
                            verificar = false;
                            suCambio = false;
                        }
                        else
                        {
                            label1.Text = "Su cambio";
                            label1.ForeColor = Color.Green;
                            totalPagar = pago - pagar;
                            verificar = true;
                            suCambio = true;
                        }
                    }
                }
                if (pago < pagar)
                {
                    label1.Text = "Pago insuficiente";
                    label1.ForeColor = Color.Red;
                    totalPagar = pagar - pago;
                    suCambio = false;
                    if (checkBox.Checked == true)
                    {
                        verificar = true;
                    }
                    else
                    {
                        verificar = false;
                    }
                }
                label2.Text = String.Format("${0:#,###,###,##0.00####}", totalPagar);
            }
            label3.Text = "Pago con";
            label3.ForeColor = Color.Teal;
        }

        public void buscarCliente(DataGridView dataGridView, string campo)
        {
            if (campo == "")
            {
                var query = Cliente.Where(c => c.ID.StartsWith(campo)); ;
                dataGridView.DataSource = query.ToList();
            }
            else
            {
                var query = Cliente.Where(c => c.ID.StartsWith(campo) || c.Nombre.StartsWith(campo));
                dataGridView.DataSource = query.ToList();
            }
            dataGridView.Columns[0].Visible = false;
        }

        public void datosCliente(CheckBox checkBox, TextBox textBox_pagos, TextBox textBox_Buscar,
            DataGridView dataGridView, List<Label> labels)
        {
            string deuda1, nombre, apelldio;
            Decimal deuda2, deudaTotal;
            if (checkBox.Checked == true)
            {
                if (textBox_pagos.Text == "")
                {
                    if (checkBox.Checked == false)
                    {
                        labels[0].Text = "$0.00";
                        labels[1].Text = "$0.00";
                        labels[2].Text = labels[0].Text;
                        labels[3].Text = "";
                        labels[4].Text = "$0.00";
                        labels[5].Text = "$0.00";
                        labels[6].Text = "--/--/--";
                    }
                        
                }
                else
                {
                    if (verificar)
                    {
                        if (textBox_Buscar.Text != "")
                        {
                            deuda1 = Convert.ToString(dataGridView.CurrentRow.Cells[5].Value);
                            deuda2 = Convert.ToDecimal(deuda1.Replace("$", ""));
                            deudaTotal = deuda2 + totalPagar;
                            labels[0].Text = string.Format("${0:#,###,###,##0.00####}", deudaTotal);
                            nombre = Convert.ToString(dataGridView.CurrentRow.Cells[2].Value);
                            apelldio = Convert.ToString(dataGridView.CurrentRow.Cells[3].Value);

                            labels[3].Text = nombre + "" + apelldio;
                            labels[1].Text = string.Format("${0:#,###,###,##0.00####}", totalPagar);
                            labels[4].Text = deuda1;
                            labels[2].Text = string.Format("${0:#,###,###,##0.00####}", deudaTotal);
                            labels[5].Text = Convert.ToString(dataGridView.CurrentRow.Cells[6].Value);
                            labels[6].Text = fecha;
                        }
                    }   
                }
            }
            else
            {
                labels[0].Text = "$0.00";
                labels[1].Text = "$0.00";
                labels[2].Text = labels[0].Text;
                labels[3].Text = "";
                labels[4].Text = "$0.00";
                labels[5].Text = "$0.00";
                labels[6].Text = "--/--/--";
                textBox_Buscar.Text = "";
            }
        }

        public bool cobrar(CheckBox checkBox, TextBox textBox_pagos, DataGridView dataGridView, List<Label> labels,
            int caja, int idUsuario)
        {
            var ultimoTicket = Ventas.OrderByDescending(v => v.NumeroTicket).ToList();
            int ultimo = ultimoTicket[0].NumeroTicket;
            bool valor = false;
            if (textBox_pagos.Text == "")
            {
                labels[7].Text = "Ingrese el pago";
                labels[7].ForeColor = Color.Red;
                textBox_pagos.Focus();
            }
            else
            {
                if (verificar) { 
                String saldoActual, IDCliente= null;
                Decimal deuda= 0, deudaActual = 0m, pagos, ingresosInicial;
                int idCliente= 0, limite;
                pagos = Convert.ToDecimal(textBox_pagos.Text);
                if (checkBox.Checked == true)
                {
                    if (dataGridView.CurrentRow != null)
                    {
                        idCliente = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);
                        IDCliente = Convert.ToString(dataGridView.CurrentRow.Cells[1].Value);
                        saldoActual = Convert.ToString(dataGridView.CurrentRow.Cells[5].Value);
                        deudaActual = Convert.ToDecimal(saldoActual.Replace("$",""));
                        deuda = totalPagar + deudaActual;
                        limite = Convert.ToInt16(dataGridView.CurrentRow.Cells[4].Value);
                        if (limite == 0)
                            {
                                insertVentas();
                            }
                        else
                            {
                                if (limite >= deuda)
                                {
                                    insertVentas();
                                }
                                else
                                {
                                    labels[8].Text = "Sobrepasa el limite de credito";
                                    valor = false;
                                }
                            }
                        
                        
                        
                    }
                    else
                    {
                            if (verificar)
                            {
                                labels[8].Text = "Seleccione un cliente";
                                valor = false;
                            }
                    }
                }
                else
                {
                    if (verificar)
                    {
                        if (pagos >= importe)
                        {
                            insertVentas();
                        }
                    }
                    else
                    {

                    }
                }
                void insertVentas()
                    {
                        var ventaTempo = TempoVentas.Where(t => t.IdUsuario.Equals(idUsuario)).ToList();
                        ultimo++;
                        if (ventaTempo.Count > 0)
                        {
                            
                            if (checkBox.Checked == true)
                            {
                                ventaTempo.ForEach(item =>
                                {
                                    Ventasclientes.Value(v => v.Codigo, item.Codigo)
                                            .Value(v => v.Descripcion, item.Descripcion)
                                            .Value(v => v.Cantidad, item.Cantidad)
                                            .Value(v => v.Fecha, fecha)
                                            .Value(v => v.Caja, caja)
                                            .Value(v => v.IdUsuario, idUsuario)
                                            .Value(v => v.IdCliente, idCliente)
                                            .Insert();
                                });

                                var reporte = ReportesClientes.Where(r => r.IdCliente.Equals(idCliente)).ToList();

                                ReportesClientes.Where(r => r.IdRegistro.Equals(reporte[0].IdRegistro))
                                .Set(r => r.SaldoActual, string.Format("${0:#,###,###,##0.00####}", deuda))
                                .Set(r => r.FechaActual, fecha)
                                .Update();

                                CreditosVentas.Value(b => b.Total, string.Format("${0:#,###,###,##0.00####}", importe))
                                          .Value(b => b.Pago, string.Format("${0:#,###,###,##0.00####}", pagos))
                                          .Value(b => b.Credito, string.Format("${0:#,###,###,##0.00####}", totalPagar))
                                          .Value(b => b.Dia, Convert.ToInt16(dia))
                                          .Value(b => b.Mes, mes)
                                          .Value(b => b.Año, año)
                                          .Value(b => b.Fecha, fecha)
                                          .Value(b => b.Cliente, IDCliente)
                                          .Value(b => b.Caja, caja)
                                          .Value(b => b.IdUsuario, idUsuario)
                                          .Insert();
                            }
                            else
                            {
                                ventaTempo.ForEach(item =>
                                {
                                    Ventas.Value(v => v.Codigo, item.Codigo)
                                            .Value(v => v.Descripcion, item.Descripcion)
                                            .Value(v => v.Cantidad, item.Cantidad)
                                            .Value(v => v.Dia, Convert.ToInt16(dia))
                                            .Value(v => v.Mes, mes)
                                            .Value(v => v.Año, año)
                                            .Value(v => v.Fecha, fecha)
                                            .Value(v => v.Caja, caja)
                                            .Value(v => v.IdUsuario, idUsuario)
                                            .Value(v => v.NumeroTicket, ultimo)
                                            .Insert();
                                });
                            }

                            var cajaIngreso = CajasIngresos.Where(t => t.Caja.Equals(caja) && t.IdUsuario.Equals(idUsuario)
                                    && t.Type.Equals("Ventas") && t.Fecha.Equals(fecha)).ToList();
                            if (cajaIngreso.Count > 0)
                            {
                                ingresos = pagos + Convert.ToDecimal(cajaIngreso[0].Ingreso.Replace("$", ""));
                                CajasIngresos.Where(c => c.Id.Equals(cajaIngreso[0].Id) && c.Caja.Equals(caja)
                                && c.IdUsuario.Equals(idUsuario) && c.Type.Equals("Ventas") && c.Fecha.Equals(fecha))
                                    .Set(c => c.Ingreso, string.Format("${0:#,###,###,##0.00####}", ingresos))
                                    .Update();
                            }
                            else
                            {
                                CajasIngresos.Value(v => v.Caja, caja)
                                        .Value(v => v.Ingreso, string.Format("${0:#,###,###,##0.00####}", pagos))
                                        .Value(v => v.Type, "Ventas")
                                        .Value(v => v.Dia, Convert.ToInt16(dia))
                                        .Value(v => v.Mes, mes)
                                        .Value(v => v.Año, año)
                                        .Value(v => v.IdUsuario, idUsuario)
                                        .Value(v => v.Fecha, fecha)
                                        .Insert();
                            }
                            valor = true;
                        }
                        if (suCambio)
                        {
                            ingresosInicial = 0;
                            var cajaIngresoInicial = CajasIngresos.Where(t => t.Caja.Equals(caja)
                            && t.IdUsuario.Equals(idUsuario)
                            && t.Type.Equals("Inicial") && t.Fecha.Equals(fecha)).ToList();
                            if (cajaIngresoInicial.Count > 0)
                            {
                                cajaIngresoInicial.ForEach(item => {

                                    ingresosInicial += Convert.ToDecimal(item.Ingreso.Replace("$", ""));
                                });
                                if (ingresosInicial > 0)
                                {
                                    if (ingresosInicial > totalPagar || ingresosInicial == totalPagar)
                                    {
                                        ingresosInicial -= totalPagar;
                                        CajasIngresos.Where(t => t.Caja.Equals(caja) 
                                        && t.Id.Equals(cajaIngresoInicial[0].Id)
                                        && t.IdUsuario.Equals(idUsuario)
                                        && t.Type.Equals("Inicial") 
                                        && t.Fecha.Equals(fecha))
                                        .Set(t => t.Ingreso, string.Format("${0:#,###,###,##0.00####}", ingresosInicial))
                                        .Update();
                                        valor = true;
                                    }
                                    else
                                    {
                                        ingresosVentas();
                                    }
                                }
                                else
                                {
                                    ingresosVentas();
                                }
                            }
                            else
                            {
                                ingresosVentas();
                            }
                            void ingresosVentas()
                            {
                                Decimal ingresosVenta = 0;
                                ingresosInicial = 0;
                                var cajaIngresoVentas = CajasIngresos.Where(c => c.Caja.Equals(caja) && c.IdUsuario.Equals(idUsuario)
                                && c.Type.Equals("Ventas") && c.Fecha.Equals(fecha)).ToList();
                                if (cajaIngresoVentas.Count > 0)
                                {
                                    var data = cajaIngresoVentas[0].Ingreso;
                                    ingresosVenta = Convert.ToDecimal(data.Replace("$", ""));
                                    if (totalPagar < ingresosVenta || ingresosVenta == totalPagar)
                                    {
                                        if (0 < cajaIngresoInicial.Count)
                                        {
                                            var ingresoIni = cajaIngresoInicial[0].Ingreso;
                                            ingresosInicial = Convert.ToDecimal(ingresoIni.Replace("$", ""));
                                            totalPagar -= ingresosInicial;

                                            CajasIngresos.Where(r => r.Id.Equals(cajaIngresoInicial[0].Id) 
                                            && r.Caja.Equals(caja) && r.IdUsuario.Equals(idUsuario) 
                                            && r.Type.Equals("Inicial") && r.Fecha.Equals(fecha))
                                            .Set(r => r.Ingreso, "$0.00")
                                            .Update();
                                        }
                                        ingresosVenta -= totalPagar;
                                        CajasIngresos.Where(t => t.Caja.Equals(caja)
                                            && t.Id.Equals(cajaIngresoVentas[0].Id)
                                            && t.IdUsuario.Equals(idUsuario)
                                            && t.Type.Equals("Ventas")
                                            && t.Fecha.Equals(fecha))
                                            .Set(t => t.Ingreso, string.Format("${0:#,###,###,##0.00####}", ingresosVenta))
                                            .Update();
                                        valor = true;
                                    }
                                    else
                                    {
                                        if (totalPagar < ingresosTotales || ingresosTotales == totalPagar)
                                        {
                                            if (0 < cajaIngresoInicial.Count)
                                            {
                                                var ingresoIni = cajaIngresoInicial[0].Ingreso;
                                                ingresosInicial = Convert.ToDecimal(ingresoIni.Replace("$", ""));
                                                totalPagar -= ingresosInicial;

                                                CajasIngresos.Where(t => t.Caja.Equals(caja)
                                                && t.Id.Equals(cajaIngresoInicial[0].Id)
                                                && t.IdUsuario.Equals(idUsuario)
                                                && t.Type.Equals("Inicial")
                                                && t.Fecha.Equals(fecha))
                                                .Set(t => t.Ingreso, "$0.00")
                                                .Update();
                                            }

                                                ingresosVenta -= totalPagar;
                                            CajasIngresos.Where(t => t.Caja.Equals(caja)
                                                && t.Id.Equals(cajaIngresoVentas[0].Id)
                                                && t.IdUsuario.Equals(idUsuario)
                                                && t.Type.Equals("Ventas")
                                                && t.Fecha.Equals(fecha))
                                                .Set(t => t.Ingreso, string.Format("${0:#,###,###,##0.00####}", ingresosVenta))
                                                .Update();
                                            valor = true;
                                        }
                                        else
                                        {
                                            labels[9].Text = "No hay ingresos";
                                            labels[9].ForeColor = Color.Red;
                                            valor = false;
                                        }
                                    }
                                }
                                else
                                {
                                    labels[9].Text = "No hay ingresos";
                                    labels[9].ForeColor = Color.Red;
                                    valor = false;
                                }
                            }

                        }
                    }
                }
            }
            return valor;
        }
        // termina el metodo cobrar

        public void ingresosCajas(Label label1, Label label2, Label label3, int caja, int idUsuario)
        {
            ingresosTotales = 0;
            var cajasIngresosInicial = CajasIngresos.Where(c => c.Caja.Equals(caja) && c.IdUsuario.Equals(idUsuario)
                    && c.Type.Equals("Inicial") && c.Fecha.Equals(fecha)).ToList();
            if (cajasIngresosInicial.Count > 0)
            {
                //var data = cajasIngresosInicial[0].Ingreso;
                //label1.Text = data;
                //ingresosTotales = Convert.ToDecimal(data.Replace("$",""));
                cajasIngresosInicial.ForEach(item => {
                    ingresosTotales += Convert.ToDecimal(item.Ingreso.Replace("$", ""));
                });

                label1.Text = "$" + ingresosTotales.ToString();
                label1.ForeColor = Color.LightSlateGray;
            }
            else
            {
                label1.Text = "$0.00";
                label1.ForeColor = Color.Red;
            }
            var cajasIngresosVentas = CajasIngresos.Where(c => c.Caja.Equals(caja) && c.IdUsuario.Equals(idUsuario)
                    && c.Type.Equals("Ventas") && c.Fecha.Equals(fecha)).ToList();
            if (cajasIngresosVentas.Count > 0)
            {
                var data = cajasIngresosVentas[0].Ingreso;
                label2.Text = data;
                ingresosTotales += Convert.ToDecimal(data.Replace("$", ""));
            }
            else
            {
                label2.Text = "$0.00";
                label2.ForeColor = Color.Red;
            }
            label3.Text = string.Format("${0:#,###,###,##0.00####}", ingresosTotales);
        }
        public List<TempoArticulos> getTempoVentas()
        {
            return TempoVentas.Where(t => t.IdUsuario.Equals(idUsuario)).ToList();
        }
    }
}
