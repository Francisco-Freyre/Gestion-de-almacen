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
    public class Inventario : Conexion
    {
        private static int idRegistro, existencia, idProducto, pageSize = 100000;
        private DataGridView dataGridView1, dataGridView2;
        private CheckBox checkBoxBdga_Agotados, checkBoxInv_MaxVentas;
        private Object[] objectProductos;
        private List<Label> labelProductos;
        private List<TextBox> textBoxProductos;
        private TabControl tabControl;
        private DateTimePicker dateTimePicker1, dateTimePicker2;

        public Inventario() { }
        public Inventario(Object[] objectProductos, List<Label> labelProductos, List<TextBox> textBoxProductos)
        {
            this.labelProductos = labelProductos;
            this.textBoxProductos = textBoxProductos;
            dataGridView1 = (DataGridView)objectProductos[0];
            checkBoxBdga_Agotados = (CheckBox)objectProductos[1];
            //numericUpDown = (NumericUpDown)objectProductos[2];
            tabControl = (TabControl)objectProductos[2];
            dataGridView2 = (DataGridView)objectProductos[3];
            dateTimePicker1 = (DateTimePicker)objectProductos[4];
            checkBoxInv_MaxVentas = (CheckBox)objectProductos[5];
            dateTimePicker2 = (DateTimePicker)objectProductos[6];
        }

        public void getProducto(string campo, int num_pagina, int reg_por_pagina, Label label, Label label2)
        {
            /*IEnumerable<Articulos> query;
            int inicio = (num_pagina - 1) * reg_por_pagina;
            decimal costo = 0, costo1 = 0, precioventa = 0, precioventa1 = 0;
            if (campo == "")
            {
                if (checkBoxBdga_Agotados.Checked == false)
                {
                    query = Producto.ToList();

                    var coso = Producto.ToList();

                    coso.ForEach(item =>
                    {
                        precioventa = item.PrecioVenta * item.Existencia;
                        costo1 += costo;
                        precioventa1 += precioventa;
                    });
                    label.Text = "$" + Convert.ToString(costo1);
                    label2.Text = "$" + Convert.ToString(precioventa1 - costo1);
                }
                else
                {
                    query = Producto.Where(p => p.Existencia <= p.Minimo).ToList();

                    var coso = Producto.Where(p => p.Existencia <= p.Minimo).ToList();

                    coso.ForEach(item =>
                    {
                        costo = item.Costo * item.Existencia;
                        precioventa = item.PrecioVenta * item.Existencia;
                        costo1 += costo;
                        precioventa1 += precioventa;
                    });
                    label.Text = "$" + Convert.ToString(costo1);
                    label2.Text = "$" + Convert.ToString(precioventa1 - costo1);
                }
                
            }
            else
            {
                if (checkBoxBdga_Agotados.Checked == false)
                {
                    query = Producto.Where(p => p.Categoria.StartsWith(campo) ||
                    p.Codigo.StartsWith(campo) || p.Departamento.StartsWith(campo)).ToList();

                    var coso = Producto.Where(p => p.Categoria.StartsWith(campo) ||
                    p.Codigo.StartsWith(campo) || p.Departamento.StartsWith(campo)).ToList();

                    coso.ForEach(item =>
                    {
                        costo = item.Costo * item.Existencia;
                        precioventa = item.PrecioVenta * item.Existencia;
                        costo1 += costo;
                        precioventa1 += precioventa;
                    });
                    label.Text = "$" + Convert.ToString(costo1);
                    label2.Text = "$" + Convert.ToString(precioventa1 - costo1);
                }
                else
                {
                    query = Producto.Where(p => p.Existencia <= p.Minimo && p.Codigo.StartsWith(campo) ||
                    p.Categoria.StartsWith(campo) && p.Existencia <= p.Minimo || 
                    p.Departamento.StartsWith(campo) && p.Existencia <= p.Minimo).ToList();

                    var coso = Producto.Where(p => p.Existencia <= p.Minimo && p.Codigo.StartsWith(campo) ||
                    p.Categoria.StartsWith(campo) && p.Existencia <= p.Minimo ||
                    p.Departamento.StartsWith(campo) && p.Existencia <= p.Minimo).ToList();

                    coso.ForEach(item =>
                    {
                        costo = item.Costo * item.Existencia;
                        precioventa = item.PrecioVenta * item.Existencia;
                        costo1 += costo;
                        precioventa1 += precioventa;
                    });
                    label.Text = "$" + Convert.ToString(costo1);
                    label2.Text = "$" + Convert.ToString(precioventa1 - costo1);
                }

                    
            }
            dataGridView1.DataSource = query.Skip(inicio).Take(reg_por_pagina).ToList(); 
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;

            */
        }

        public void restablecerInventario(Label label, Label label2)
        {
            idRegistro = 0;
            existencia = 0;
            idProducto = 0;
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    textBoxProductos[0].Text = "";
                    labelProductos[0].Text = "Existencia";
                    labelProductos[0].ForeColor = Color.LightSlateGray;
                    getProducto("", 1, 100, label, label2);
                    new Paginador(dataGridView1, labelProductos[1], 7, 0).primero();
                    break;
                case 1:
                    buscarVentas("", 1, pageSize);
                    //new Paginador(dataGridView2, labelProductos[2], 8, 0).primero();
                    break;
            }
            
        }

        public void dataGridViewProductos()
        {
            idRegistro = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            existencia = Convert.ToInt16(dataGridView1.CurrentRow.Cells[5].Value);
            textBoxProductos[0].Text = existencia.ToString();
        }

        public void updateExistencia(Label label1, Label label2)
        {
             if (textBoxProductos[0].Text == "")
            {
                labelProductos[0].Text = "Ingrese el dato en el campo";
                labelProductos[0].ForeColor = Color.Red;
                labelProductos[0].Focus();
            }
             else
            {
                Producto.Where(b => b.IdArticulo.Equals(idRegistro))
                                    .Set(b => b.Existencia, Convert.ToInt16(textBoxProductos[0].Text))
                                    .Update();
                restablecerInventario(label1, label2);
            }
        }

        public List<Articulos> getInventario()
        {
            return Producto.ToList();
        }

        public int buscarVentas(string campo, int num_pagina, int reg_por_pagina)
        {
            List<Ventas> query = new List<Ventas>();
            var fecha_inicio = dateTimePicker1.Value.Date.ToString("dd/MMM/yyy");
            int inicio = (num_pagina - 1) * reg_por_pagina;
            int valor = 0;
            if (campo == "")
            {
                if (checkBoxInv_MaxVentas.Checked == true)
                {
                    if (DateTime.Compare(dateTimePicker2.Value.Date, dateTimePicker1.Value.Date) < 0)
                    {
                        MessageBox.Show("\nLa fecha final debe ser mayor a la fecha inicial\n");
                    }
                    else
                    {
                        query = maxVentas(filtrarProductosFechas(fecha_inicio)).Where(c => c.Codigo.Contains(campo) ||
                        c.Departamento.Contains(campo) || c.Categoria.Contains(campo)).ToList();
                        valor = query.Count;
                    }
                }
                else
                {
                    if (DateTime.Compare(dateTimePicker2.Value.Date, dateTimePicker1.Value.Date) < 0)
                    {
                        MessageBox.Show("\nLa fecha final debe ser mayor a la fecha inicial\n");
                    }
                    else
                    {
                        query = filtrarProductosFechas(fecha_inicio).Where(c => c.Codigo.Contains(campo) ||
                        c.Departamento.Contains(campo) || c.Categoria.Contains(campo)).ToList();
                        valor = query.Count;
                    }
                }
            }
            else
            {
                if (checkBoxInv_MaxVentas.Checked == true)
                {
                    if (DateTime.Compare(dateTimePicker2.Value.Date, dateTimePicker1.Value.Date) < 0)
                    {
                        MessageBox.Show("\nLa fecha final debe ser mayor a la fecha inicial\n");
                    }
                    else
                    {
                        query = maxVentas(filtrarProductosFechas(fecha_inicio)).Where(c => c.Codigo.Contains(campo) ||
                        c.Departamento.Contains(campo) || c.Categoria.Contains(campo)).ToList();
                        valor = query.Count;
                    }

                }
                else
                {
                    if (DateTime.Compare(dateTimePicker2.Value.Date, dateTimePicker1.Value.Date) < 0)
                    {
                        MessageBox.Show("\nLa fecha final debe ser mayor a la fecha inicial\n");
                    }
                    else
                    {
                        query = filtrarProductosFechas(fecha_inicio).Where(c => c.Codigo.Contains(campo) ||
                        c.Departamento.Contains(campo) || c.Categoria.Contains(campo)).ToList();
                        valor = query.Count;
                    }
                }
            }
            dataGridView2.DataSource = query.Skip(inicio).Take(100000).ToList();
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.Columns[10].Visible = false;
            dataGridView2.Columns[11].Visible = false;
            dataGridView2.Columns[12].Visible = false;


            return valor;
        }

        private List<Ventas> maxVentas(IEnumerable<Ventas> query)
        {
            List<Ventas> listProduct = new List<Ventas>();
            foreach (var item in query)
            {
                if (verificar(item))
                {
                    listProduct.Add(new Ventas
                    {
                        IdVenta = item.IdVenta,
                        Codigo = item.Codigo,
                        Descripcion = item.Descripcion,
                        Precio = item.Precio,
                        Cantidad = item.Cantidad,
                        Importe = item.Importe,
                        Dia = item.Dia,
                        Mes = item.Mes,
                        Año = item.Año,
                        Fecha = item.Fecha,
                        Caja = item.Caja,
                        IdUsuario = item.IdUsuario,
                        NumeroTicket = item.NumeroTicket,
                        Costo = item.Costo,
                        Hora = item.Hora,
                        Departamento = item.Departamento,
                        Categoria = item.Categoria
                    });
                }
            }
            bool verificar(Ventas data)
            {
                int pos = 0, cant;
                Decimal importe1, importe2, importe3;
                foreach (var item in listProduct)
                {
                    if (item.Codigo.Equals(data.Codigo))
                    {
                        importe1 = Convert.ToDecimal(item.Importe.Replace("$", ""));
                        importe2 = Convert.ToDecimal(data.Importe.Replace("$", ""));
                        importe3 = importe1 + importe2;
                        var imporetes = String.Format("${0: #,###,###,##0.00####}", importe3);
                        cant = item.Cantidad + data.Cantidad;
                        listProduct.RemoveAt(pos);
                        listProduct.Insert(pos, new Ventas
                        {
                            IdVenta = item.IdVenta,
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion,
                            Precio = item.Precio,
                            Cantidad = cant,
                            Importe = imporetes.Replace(" ", ""),
                            Dia = item.Dia,
                            Mes = item.Mes,
                            Año = item.Año,
                            Fecha = item.Fecha,
                            Caja = item.Caja,
                            IdUsuario = item.IdUsuario,
                            NumeroTicket = item.NumeroTicket,
                            Costo = item.Costo,
                            Hora = item.Hora,
                            Departamento = item.Departamento,
                            Categoria = item.Categoria
                        });
                        return false;
                    }
                    pos++;
                    cant = 0;
                }
                return true;
            }
            return listProduct.OrderBy(p => p.Cantidad).Reverse().ToList();
        }

        private List<Ventas> filtrarProductosFechas(string fecha_inicio)
        {
            List<Ventas> listProduct = new List<Ventas>();
            var listdb1 = Ventas.Where(c => c.Fecha.Equals(fecha_inicio)).ToList();
            if (0 < listdb1.Count)
            {
                var listdb2 = Ventas.Where(c => c.IdVenta >= listdb1[0].IdVenta).ToList();
                foreach (var item in listdb2)
                {
                    if (DateTime.Compare(dateTimePicker2.Value.Date, DateTime.Parse(item.Fecha)) > 0 || 
                        DateTime.Compare(dateTimePicker2.Value.Date, DateTime.Parse(item.Fecha)) == 0)
                    {
                        listProduct.Add(new Ventas
                        {
                            IdVenta = item.IdVenta,
                            Codigo = item.Codigo,
                            Descripcion = item.Descripcion,
                            Precio = item.Precio,
                            Cantidad = item.Cantidad,
                            Importe = item.Importe,
                            Dia = item.Dia,
                            Mes = item.Mes,
                            Año = item.Año,
                            Fecha = item.Fecha,
                            Caja = item.Caja,
                            IdUsuario = item.IdUsuario,
                            NumeroTicket = item.NumeroTicket,
                            Costo = item.Costo,
                            Hora = item.Hora,
                            Departamento = item.Departamento,
                            Categoria = item.Categoria
                        });
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return listProduct;
        }
    }
}
