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
    public class Caja : Conexion
    {
        private string dia = DateTime.Now.ToString("dd");
        private string mes = DateTime.Now.ToString("MMM");
        private string year = DateTime.Now.ToString("yyy");
        private int mesint = DateTime.Now.Month;
        private DataGridView dataGridView, dataGridView2;
        private DateTimePicker dateTimePicker;
        private List<Label> labelList;
        private List<TextBox> textBoxList;
        private static int idCaja, numCaja, idUsuario, idIngreso;
        private String ingresos = null, importIngresos;
        private Decimal cajaIngreso, retirarIngresos, newIngresos;

        public Caja()
        {

        }
        public Caja(object[] objectCajas, List<Label> labelCajas, List<TextBox> textBoxCajas)
        {
            dataGridView = (DataGridView)objectCajas[0];
            dateTimePicker = (DateTimePicker)objectCajas[1];
            idUsuario = (int)objectCajas[2];
            dataGridView2 = (DataGridView)objectCajas[3];
            labelList = labelCajas;
            textBoxList = textBoxCajas;
        }
        public List<Cajas> getCaja()
        {
            return Cajas.Where(c => c.Estado == true).ToList();
        }

        public void actulizarCaja(int idCaja, bool estado)
        {
            Cajas.Where(c => c.IdCaja == idCaja)
                .Set(c => c.Estado, estado)
                .Update();
        }
        public void getIngresos()
        {
            var fecha_inicio = dateTimePicker.Value.Date.ToString("dd/MMM/yyy");
            dataGridView.DataSource = CajasIngresos.Where(c => c.Type.Equals("Ventas") && c.Fecha.Equals(fecha_inicio)).ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[7].Visible = false;
            dataGridView.Columns[2].DefaultCellStyle.ForeColor = Color.Green;
        }
        public void restablecerCajas()
        {
            idCaja = 0;
            numCaja = 0;
            textBoxList[0].Text = "";
            labelList[0].Text = "$0.00";
            labelList[2].Text = "$0.00";
            dateTimePicker.Value = new DateTime(Convert.ToInt16(year), mesint, Convert.ToInt16(dia));
            getIngresos();
            getCajas();
            labelList[3].Text = "#0";
            textBoxList[1].Text = "";
        }

        public void dataGridViewCaja()
        {
            idCaja = Convert.ToInt16(dataGridView.CurrentRow.Cells[0].Value);
            numCaja = Convert.ToInt16(dataGridView.CurrentRow.Cells[1].Value);
            ingresos = Convert.ToString(dataGridView.CurrentRow.Cells[2].Value);
            labelList[0].Text = ingresos;
            labelList[2].Text = ingresos;
            if (textBoxList[0].Text.Equals(""))
            {
                labelList[1].Text = "Retirar ingresos";
                labelList[1].ForeColor = Color.LightSlateGray;
            }
            else
            {
                labelList[1].Text = "Retirar ingresos";
                labelList[1].ForeColor = Color.Green;
                cajaIngresos();
            }
        }

        public bool cajaIngresos()
        {
            var valor = false;
            if (ingresos != null)
            {
                if (textBoxList[0].Text.Equals(""))
                {
                    labelList[1].Text = "Retirar ingresos";
                    labelList[1].ForeColor = Color.LightSlateGray;
                    retirarIngrsos();
                    valor = false;
                }
                else
                {
                    labelList[1].Text = "Retirar ingresos";
                    labelList[1].ForeColor = Color.Green;
                    var ingreso1 = Convert.ToDecimal(ingresos.Replace("$", ""));
                    var ingreso2 = Convert.ToDecimal(textBoxList[0].Text);
                    if (numCaja == 0)
                    {
                        if (ingreso1 >= ingreso2)
                        {
                            retirarIngrsos();
                            valor = true;
                        }
                        else
                        {
                            labelList[1].Text = "Se a sobrepasado del ingreso";
                            labelList[1].ForeColor = Color.Red;
                            valor = false;
                        }
                    }
                    else
                    {
                        var caja = Cajas.Where(c => c.Caja.Equals(numCaja)).ToList();
                        if (caja[0].Estado)
                        {
                            if (ingreso1 >= ingreso2)
                            {
                                retirarIngrsos();
                                valor = true;
                            }
                            else
                            {
                                labelList[1].Text = "Se a sobrepasado del ingreso";
                                labelList[1].ForeColor = Color.Red;
                                valor = false;
                            }
                        }
                        else
                        {
                            if (ingreso1 > 100)
                            {
                                var ingresos = ingreso1 - ingreso2;
                                if (ingresos <= 100)
                                {
                                    var retirar = ingreso1 - 100;
                                    var data = String.Format("${0: #,###,###,##0.00####}", retirar);
                                    labelList[1].Text = "Solo puede retirar " + data.Replace(" ", "");
                                    labelList[1].ForeColor = Color.Red;
                                    labelList[2].Text = data.Replace(" ", "");
                                    if (retirar == ingreso2)
                                    {
                                        valor = true;
                                    }
                                    else
                                    {
                                        valor = false;
                                    }
                                }
                                else
                                {
                                    retirarIngrsos();
                                    valor = true;
                                }
                            }
                            else
                            {
                                labelList[1].Text = "No se pueden retirar los ingresos";
                                labelList[1].ForeColor = Color.Red;
                                valor = false;
                            }
                        }
                    }
                }
            }
            else
            {
                labelList[1].Text = "Seleccione un ingresos";
                labelList[1].ForeColor = Color.Red;
                valor = false;
            }
            return valor;
        }
        private void retirarIngrsos()
        {
            if (textBoxList[0].Text.Equals(""))
            {
                labelList[2].Text = ingresos;
            }
            else
            {
                cajaIngreso = Convert.ToDecimal(ingresos.Replace("$", ""));
                retirarIngresos = Convert.ToDecimal(textBoxList[0].Text);
                newIngresos = cajaIngreso - retirarIngresos;
                importIngresos = String.Format("${0: #,###,###,##0.00####}", newIngresos);
                labelList[2].Text = importIngresos.Replace(" ", "");
            }
        }

        public void guardarIngresos()
        {
            CajasIngresos.Where(r => r.Id.Equals(idCaja) && r.Caja.Equals(numCaja))
                                          .Set(r => r.Ingreso, importIngresos.Replace(" ", ""))
                                          .Update();
            var usuario = Usuario.Where(u => u.IdUsuario.Equals(idUsuario)).ToList();
            var ingresos = String.Format("${0: #,###,###,##0.00####}", retirarIngresos);
            ReportesIngresos.Value(r => r.Amin, usuario[0].Nombre + " " + usuario[0].Apellido)
                           .Value(r => r.Caja, numCaja)
                           .Value(r => r.Ingreso, ingresos.Replace(" ", ""))
                           .Value(r => r.Fecha, dia + "/" + mes + "/" + year)
                           .Insert();
            restablecerCajas();
            idUsuario = 0;
        }

        public async void getCajas()
        {
            List<TCaja> cajas = new List<TCaja>();
            String estado, inicial;
           await Cajas.ForEachAsync(item =>
            {
                if (item.Estado.Equals(true))
                {
                    estado = "Disponible";
                    inicial = "$0.00";
                }
                else
                {
                    estado = "Activa";
                    using (var db = new Conexion())
                    {
                        var cajaIngreso = db.CajasIngresos.Where(c => c.Caja.Equals(item.Caja)
                                           && c.Type.Equals("Inicial") && c.Fecha.Equals(dia + "/" + mes + "/" + year)).ToList();
                        if (cajaIngreso.Count.Equals(0))
                        {
                            inicial = "$0.00";
                        }
                        else
                        {
                            Decimal ingresos = 0;
                            cajaIngreso.ForEach(items => {
                                ingresos += Convert.ToDecimal(items.Ingreso.Replace("$", ""));
                            });
                            inicial = string.Format("${0:#,###,###,##0.00####}", ingresos);
                        }
                    }
                }
                cajas.Add(new TCaja
                {
                    IdCaja = item.IdCaja,
                    Caja = item.Caja,
                    Estado = estado,
                    Inicial = inicial
                });
            });
            dataGridView2.DataSource = cajas.ToList();
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[3].DefaultCellStyle.ForeColor = Color.Green;
        }

        public void dataGridViewCajaIngresos()
        {
            idIngreso = Convert.ToInt16(dataGridView2.CurrentRow.Cells[0].Value);
            numCaja = Convert.ToInt16(dataGridView2.CurrentRow.Cells[1].Value);
            labelList[3].Text = "#" + Convert.ToString(numCaja);
        }

        public void insetarIngresoInicial()
        {
            var cajaRegistro = cajastemporal.Where(c => c.Caja.Equals(numCaja) && c.Fecha.Equals(dia + "/" + mes + "/" + year)).ToList();

            var ingresosIniciales = CajasIngresos.Where(i => i.Caja.Equals(numCaja) && i.Fecha.Equals(dia + "/" + mes + "/" + year) && i.Type.Equals("Inicial")).ToList();

            var ingresoinicial = Convert.ToDecimal(textBoxList[1].Text);
            if (0 < ingresosIniciales.Count)
            {
                ingresoinicial += Convert.ToDecimal(ingresosIniciales[0].Ingreso.Replace("$", ""));
                CajasIngresos.Where(b => b.Id.Equals(ingresosIniciales[0].Id))
                    .Set(b => b.Ingreso, string.Format("${0:#,###,###,##0.00####}", ingresoinicial))
                    .Update();
            }
            else
            {
                CajasIngresos.Value(b => b.Caja, numCaja)
                                         .Value(b => b.Ingreso, string.Format("${0:#,###,###,##0.00####}", ingresoinicial))
                                         .Value(b => b.Type, "Inicial")
                                         .Value(b => b.IdUsuario, cajaRegistro[0].IdUsuario)
                                         .Value(b => b.Dia, Convert.ToInt16(dia))
                                         .Value(b => b.Mes, mes)
                                         .Value(b => b.Año, year)
                                         .Value(b => b.Fecha, dia + "/" + mes + "/" + year)
                                         .Insert();
            }

            restablecerCajas();
        }

        public void guardarIngresosEntrada(int caja, string ingreso, int dia, string mes, string año, int idUsuario, string fecha)
        {
            
            CajasIngresos.Value(b => b.Caja, caja)
                                         .Value(b => b.Ingreso, string.Format("${0:#,###,###,##0.00####}", ingreso))
                                         .Value(b => b.Type, "Inicial")
                                         .Value(b => b.IdUsuario, idUsuario)
                                         .Value(b => b.Dia, Convert.ToInt16(dia))
                                         .Value(b => b.Mes, mes)
                                         .Value(b => b.Año, año)
                                         .Value(b => b.Fecha, fecha)
                                         .Insert();
        }

        public void guardarDineroCaja(int caja, string ingreso, int idUsuario, string fecha)
        {
            entradasIniciales.Value(b => b.Caja, caja)
                .Value(b => b.Ingreso, string.Format("${0:#,###,###,##0.00####}", ingreso))
                .Value(b => b.Type, "Inicial")
                .Value(b => b.IdUsuario, idUsuario)
                .Value(b => b.Fecha, fecha)
                .Insert();
        }

        public void salidasIngresos(int idUsuario, int caja, string fecha, string retiro, string motivo)
        {
            string ingresoAnterior2;
            double ingresoAnterior, Retirar1, retirar2;
            var cajaRegistro = CajasIngresos.Where(c => c.Fecha.Equals(fecha)
            && c.IdUsuario.Equals(idUsuario) && c.Type.Equals("Ventas")).ToList();
            if (cajaRegistro.Count != 0)
            {
                ingresoAnterior2 = cajaRegistro[0].Ingreso.Replace("$", "");
                ingresoAnterior = Convert.ToDouble(ingresoAnterior2);
                Retirar1 = Convert.ToDouble(retiro.Replace("$", ""));
                if (Retirar1 >= ingresoAnterior)
                {
                    MessageBox.Show("El monto de retiro tiene que ser menor.", "Punto Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    retirar2 = ingresoAnterior - Retirar1;
                    CajasIngresos.Where(b => b.IdUsuario.Equals(idUsuario)
                       && b.Type.Equals("Ventas") && b.Fecha.Equals(fecha))
                      .Set(b => b.Ingreso, string.Format("${0:#,###,###,##0.00####}", retirar2))
                      .Update();

                    salidasdinero.Value(s => s.Monto, string.Format("${0:#,###,###,##0.00####}", Retirar1))
                        .Value(s => s.Motivo, motivo)
                        .Value(s => s.IdUsuario, idUsuario)
                        .Value(s => s.Fecha, fecha)
                        .Insert();
                }
            }
            else
            {
                MessageBox.Show("La lista esta sola.", "Punto Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
           
        }

        class TCaja
        {
            public int IdCaja { set; get; }
            public int Caja { set; get; }
            public String Estado { set; get; }
            public String Inicial { set; get; }
        }
    }
}
