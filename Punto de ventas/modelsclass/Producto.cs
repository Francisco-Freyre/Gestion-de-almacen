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
    public class Producto : Conexion
    {

        public List<Departamentos> getDepartamentos()
        {
            return Departamento.ToList();
        }

        internal object getCategorias(int dpto)
        {
            var query = Categoria.Where(c => c.IdDpto.Equals(dpto));
            return query.ToList();
        }

        public List<Categorias> getCategoria()
        {
            return Categoria.ToList();
        }

        public List<Articulos> getProductos()
        {
            return Producto.ToList();
        }

        internal void guardarProducto(string codigo, string descripcion, decimal precioventa, int existencia)
        {
            var valorProducto = Producto.Where(p => p.Codigo == codigo).ToList();
            if (0 < valorProducto.Count)
            {
                Producto.Where(p => p.IdArticulo == valorProducto[0].IdArticulo)
                    .Set(p => p.Codigo, codigo)
                    .Set(p => p.Descripcion, descripcion)
                    .Set(p => p.Precio, precioventa)
                    .Set(p => p.Existencia, existencia)
                    .Update();
                /*
                ReportesMoviminetos.Value(r => r.Codigo, valorProducto[0].Codigo)
                    .Value(r => r.Descripcion, valorProducto[0].Descripcion)
                    .Value(r => r.CostoAnterior, valorProducto[0].Costo)
                    .Value(r => r.PrecioVentaAnterior, valorProducto[0].PrecioVenta)
                    .Value(r => r.ExistenciaAnterior, valorProducto[0].Existencia)
                    .Value(r => r.DepartamentoAnterior, valorProducto[0].Departamento)
                    .Value(r => r.CategoriaAnterior, valorProducto[0].Categoria)
                    .Value(r => r.PrecioVentaNuevo, precioventa)
                    .Value(r => r.ExistenciaNueva, existencia)
                    .Insert();*/
            }
            else
            {
                Producto
                    .Value(p => p.Codigo, codigo)
                    .Value(p => p.Descripcion, descripcion)
                    .Value(p => p.Precio, precioventa)
                    .Value(p => p.Existencia, existencia)
                    .Insert();

                /*
                ReportesMoviminetos.Value(r => r.Codigo, codigo)
                    .Value(r => r.Descripcion, descripcion)
                    .Value(r => r.CostoAnterior, 0)
                    .Value(r => r.PrecioVentaAnterior, 0)
                    .Value(r => r.ExistenciaAnterior, 0)
                    .Value(r => r.DepartamentoAnterior, "Ninguno")
                    .Value(r => r.CategoriaAnterior, "Ninguno")
                    .Value(r => r.PrecioVentaNuevo, precioventa)
                    .Value(r => r.ExistenciaNueva, existencia)
                    .Insert();*/
            }
        }

        public void buscarProducto(DataGridView dataGridView, string campo, int num_pagina, int reg_por_pagina)
        {
            IEnumerable<Articulos> query;
            int inicio = (num_pagina - 1) * reg_por_pagina;
            if (campo == "")
            {
                query = Producto.ToList(); 
            }
            else
            {
                query = Producto.Where(p => p.Descripcion.StartsWith(campo) || p.Codigo.StartsWith(campo));
            }
            dataGridView.DataSource = query.Skip(inicio).Take(reg_por_pagina).ToList();
            dataGridView.Columns[0].Visible = false;
        }

        internal void actualizarProducto(int idProducto, string codigo, string descripcion, decimal precioventa, int existencia)
        {
            var valorProducto = Producto.Where(p => p.IdArticulo == idProducto).ToList();
            Producto.Where(p => p.IdArticulo == idProducto)
                    .Set(p => p.Codigo, codigo)
                    .Set(p => p.Descripcion, descripcion)
                    .Set(p => p.Precio, precioventa)
                    .Set(p => p.Existencia, existencia)
                    .Update();

            /*ReportesMoviminetos.Value(r => r.Codigo, valorProducto[0].Codigo)
                    .Value(r => r.Descripcion, valorProducto[0].Descripcion)
                    .Value(r => r.PrecioVentaAnterior, valorProducto[0].Precio)
                    .Value(r => r.ExistenciaAnterior, valorProducto[0].Existencia)
                    .Value(r => r.PrecioVentaNuevo, precioventa)
                    .Value(r => r.ExistenciaNueva, existencia)
                    .Insert();*/
        }

        internal void borrarProducto(int idProducto, string fecha)
        {
            var valorProducto = Producto.Where(p => p.IdArticulo == idProducto).ToList();
            Producto.Where(p => p.IdArticulo == idProducto).Delete();

            /*ReportesMoviminetos.Value(r => r.Codigo, valorProducto[0].Codigo)
                    .Value(r => r.Descripcion, valorProducto[0].Descripcion)
                    .Value(r => r.PrecioVentaAnterior, valorProducto[0].Precio)
                    .Value(r => r.ExistenciaAnterior, valorProducto[0].Existencia)
                    .Value(r => r.CostoNuevo, 0)
                    .Value(r => r.PrecioVentaNuevo, 0)
                    .Value(r => r.ExistenciaNueva, 0)
                    .Value(r => r.DepartementoNuevo, "Ninguno")
                    .Value(r => r.CategoriaNuevo, "Ninguno")
                    .Value(r => r.Fecha, fecha)
                    .Insert();*/
        }

        public void buscarProductoInventario(string codigo, Label label1, Label label2)
        {
            var producto = Producto.Where(p => p.Codigo == codigo).ToList();
            if (producto.Count == 0)
            {
                label1.Text = "";
                label2.Text = "";
            }
            else
            {
                label1.Text = Convert.ToString(producto[0].Descripcion);
                label2.Text = Convert.ToString(producto[0].Existencia);
            }     
        }

        public void actualizarProductoInventario(string codigo, string sumar, string label, Label label1, string usuario)
        {
            string fecha = DateTime.Now.ToString("dd/MMM/yyy");
            string hora = DateTime.Now.ToString("hh:mm:ss");
        var valorProducto = Producto.Where(p => p.Codigo == codigo).ToList();
            int existencia, existencia2, existencia3;
            existencia = Convert.ToInt16(label);
            existencia2 = Convert.ToInt16(sumar);
            existencia3 = existencia + existencia2;
            if (existencia2 >= 0)
            {
                Producto.Where(p => p.Codigo == codigo)
                    .Set(p => p.Existencia, existencia3)
                    .Update();

                Movimientos.Value(p => p.Codigo, codigo)
                               .Value(p => p.Descripcion, valorProducto[0].Descripcion)
                               .Value(p => p.Cantidad, existencia2)
                               .Value(p => p.TipoMovimiento, "Alta")
                               .Value(p => p.Fecha, fecha)
                               .Value(p => p.Hora, hora)
                               .Value(p => p.Usuario, usuario)
                               .Insert();
            }
            else
            {
                label1.Text = "No se pueden ingresar numeros negativos";
                label1.ForeColor = Color.Red;
            }
        }

        public void buscarProductoVentas(DataGridView dataGridView, string campo, int num_pagina, int reg_por_pagina)
        {
            IEnumerable<Articulos> query;
            int inicio = (num_pagina - 1) * reg_por_pagina;
            if (campo == "")
            {
                query = Producto.ToList();
            }
            else
            {
                query = Producto.Where(p => p.Descripcion.Contains(campo) || p.Codigo.Contains(campo));
            }
            dataGridView.DataSource = query.Skip(inicio).Take(reg_por_pagina).ToList();
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[3].Visible = false;
            dataGridView.Columns[6].Visible = false;
            dataGridView.Columns[9].Visible = false;
            dataGridView.Columns[10].Visible = false;
        }

    }
}
