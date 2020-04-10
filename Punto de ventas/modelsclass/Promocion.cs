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
    public class Promocion : Conexion
    {
        public void insertarPromo(string codigo, int cantidad, int precio, string descripcion)
        {
            var promocion = Promos.Where(p => p.Codigo.Equals(codigo)).ToList();

            if (promocion.Count == 0)
            {
                Promos.Value(p => p.Codigo, codigo)
                      .Value(p => p.CantidadProductos, cantidad)
                      .Value(p => p.Precio, precio)
                      .Value(p => p.Descripcion, descripcion)
                      .Insert();
            }
            else
            {
                Promos.Where(p => p.Codigo.Equals(codigo))
                      .Set(p => p.CantidadProductos, cantidad)
                      .Set(p => p.Precio, precio)
                      .Set(p => p.Descripcion, descripcion)
                      .Update();
            }
        }

        public void mostrarGrid(string campo, DataGridView dataGridView)
        {
            IEnumerable<Promociones> datos;

            if (campo == "")
            {
                 datos = Promos.ToList();
            }
            else
            {
                 datos = Promos.Where(p => p.Codigo.Contains(campo) || p.Descripcion.Contains(campo)).ToList();
            }
            dataGridView.DataSource = datos.ToList();
            dataGridView.Columns[0].Visible = false;
        }

        public void eliminarRegistro(string codigo)
        {
            var datos = Promos.Where(p => p.Codigo.Equals(codigo)).ToList();
            if (datos.Count != 0)
            {
                Promos.Where(p => p.Codigo.Equals(datos[0].Codigo)).Delete();
            }
        }
        //Promociones segunda tabla #######################################################################################################
        public void insertarPromoDos(string codigo, int cantidad, int precio, string descripcion, int limite)
        {
            var promocion = PromosDos.Where(p => p.Codigo.Equals(codigo)).ToList();

            if (promocion.Count == 0)
            {
                PromosDos.Value(p => p.Codigo, codigo)
                      .Value(p => p.CantidadProductos, cantidad)
                      .Value(p => p.Precio, precio)
                      .Value(p => p.Descripcion, descripcion)
                      .Value(p => p.LimiteVenta, limite)
                      .Insert();
            }
            else
            {
                PromosDos.Where(p => p.Codigo.Equals(codigo))
                      .Set(p => p.CantidadProductos, cantidad)
                      .Set(p => p.Precio, precio)
                      .Set(p => p.Descripcion, descripcion)
                      .Set(p => p.LimiteVenta, limite)
                      .Update();
            }

        }
        public void mostrarGridDos(string campo, DataGridView dataGridView)
        {
            IEnumerable<Promociones_Dos> datos;

            if (campo == "")
            {
                datos = PromosDos.ToList();
            }
            else
            {
                datos = PromosDos.Where(p => p.Codigo.Contains(campo) || p.Descripcion.Contains(campo)).ToList();
            }
            dataGridView.DataSource = datos.ToList();
            dataGridView.Columns[0].Visible = false;
        }
        public void eliminarRegistroDos(string codigo)
        {
            var datos = PromosDos.Where(p => p.Codigo.Equals(codigo)).ToList();
            if (datos.Count != 0)
            {
                PromosDos.Where(p => p.Codigo.Equals(datos[0].Codigo)).Delete();
            }
        }

    }
}
