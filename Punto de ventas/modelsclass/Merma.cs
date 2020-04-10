using LinqToDB;
using Punto_de_ventas.Connection;
using Punto_de_ventas.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas.modelsclass
{
    public class Merma : Conexion
    {
        public void insertarMerma(string codigo, int cantidad, string fecha, string hora, string usuario)
        {
            var mermas = Mermas.Where(p => p.Codigo.Equals(codigo)).ToList();
            var producto = Producto.Where(p => p.Codigo.Equals(codigo)).ToList();

            if (producto.Count != 0)
            {
                if (mermas.Count != 0)
                {
                    int cant = mermas[0].Cantidad + cantidad;
                    Mermas.Where(p => p.Codigo.Equals(mermas[0].Codigo))
                      .Set(p => p.Cantidad, cant)
                      .Set(p => p.Fecha, fecha)
                      .Set(p => p.Hora, hora)
                      .Update();

                    Producto.Where(p => p.Codigo.Equals(codigo))
                            .Set(p => p.Existencia, producto[0].Existencia-cantidad)
                            .Update();

                    Movimientos.Value(p => p.Codigo, codigo)
                               .Value(p => p.Descripcion, producto[0].Descripcion)
                               .Value(p => p.Cantidad, cantidad)
                               .Value(p => p.TipoMovimiento, "Merma")
                               .Value(p => p.Fecha, fecha)
                               .Value(p => p.Hora, hora)
                               .Value(p => p.Usuario, usuario)
                               .Insert();
                }
                else
                {
                    Mermas.Value(p => p.Codigo, codigo)
                      .Value(p => p.Descripcion, producto[0].Descripcion)
                      .Value(p => p.Cantidad, cantidad)
                      .Value(p => p.Fecha, fecha)
                      .Value(p => p.Hora, hora)
                      .Insert();

                    Producto.Where(p => p.Codigo.Equals(codigo))
                            .Set(p => p.Existencia, producto[0].Existencia - cantidad)
                            .Update();

                    Movimientos.Value(p => p.Codigo, codigo)
                               .Value(p => p.Descripcion, producto[0].Descripcion)
                               .Value(p => p.Cantidad, cantidad)
                               .Value(p => p.TipoMovimiento, "Merma")
                               .Value(p => p.Fecha, fecha)
                               .Value(p => p.Hora, hora)
                               .Value(p => p.Usuario, usuario)
                               .Insert();
                }
                
            }
        }

        public void mostrarGrid(string campo, DataGridView dataGridView)
        {
            IEnumerable<Mermas> datos;

            if (campo == "")
            {
                datos = Mermas.ToList();
            }
            else
            {
                datos = Mermas.Where(p => p.Codigo.Contains(campo) || p.Descripcion.Contains(campo)).ToList();
            }
            dataGridView.DataSource = datos.ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[4].Visible = false;
            dataGridView.Columns[5].Visible = false;
        }

        public void eliminarRegistro(string codigo)
        {
            var datos = Mermas.Where(p => p.Codigo.Equals(codigo)).ToList();
            if (datos.Count != 0)
            {
                Mermas.Where(p => p.Codigo.Equals(datos[0].Codigo)).Delete();
            }
        }

        public void insertarPedido(string fecha, string desc, int cantidad, string provedor)
        {
            
            var pedido = pedidos.Where(p => p.Descripcion.Equals(desc) && p.Descripcion.Equals(cantidad)).ToList();

            if (pedido.Count != 0)
            {
                pedidos.Where(p => p.IdPedido.Equals(pedido[0].IdPedido))
                        .Set(p => p.Descripcion, desc)
                        .Set(p => p.Cantidad, cantidad)
                        .Set(p => p.Proveedor, provedor)
                       .Update();  
            }
            else
            {
                pedidos.Value(p => p.Descripcion, desc)
                       .Value(p => p.Cantidad, cantidad)
                       .Value(p => p.Fecha, fecha)
                       .Value(p => p.Proveedor, provedor)
                       .Insert();
            }
        }

        public void mostrarGridPedido(DateTimePicker dateTimePicker, DataGridView dataGridView, string provedor)
        {
            var fecha_inicio = dateTimePicker.Value.Date.ToString("dd/MMM/yyy");
            IEnumerable<Pedidos> datos;

            if (fecha_inicio == "")
            {
                datos = pedidos.ToList();
            }
            else
            {
                datos = pedidos.Where(p => p.Fecha.Equals(fecha_inicio) && p.Proveedor.Equals(provedor)).ToList();
            }
            dataGridView.DataSource = datos.ToList();
            dataGridView.Columns[0].Visible = false;
        }

        public void eliminarPedido(int id)
        {
            var datos = pedidos.Where(p => p.IdPedido.Equals(id)).ToList();
            if (datos.Count != 0)
            {
                pedidos.Where(p => p.IdPedido.Equals(datos[0].IdPedido)).Delete();
            }
        }
    }
}