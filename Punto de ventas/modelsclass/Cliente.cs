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
    public class Cliente : Conexion
    {
        public List<ReportesClientes> reporte;
        public List<Personal> getClientes()
        {
            var query = from c in Cliente
                        select c;
            return query.ToList();
        }
        public void insertarCliente(string id, string nombre, string apellido, string telefono)
        {
            int pos, idCliente;
            using (var db = new Conexion())
            {
                db.Insert(new Personal()
                {
                    ID=id,
                    Nombre = nombre,
                    Apellido = apellido,
                    Telefono = telefono,
                });
                List<Personal> cliente = getClientes();
                pos = cliente.Count;
                pos--;
                idCliente = cliente[pos].IdPersonal;
                /*db.Insert(new ReportesClientes()
                {
                    IdCliente = idCliente,
                    SaldoActual="$0.00",
                    FechaActual="Sin fecha",
                    UltimoPago="$0.00",
                    FechaPago="Sin fecha",
                    ID=id
                });*/
            }
        }

        public void buscarCliente(DataGridView DataGridView, string campo, int num_pagina, int reg_por_pagina)
        {
            IEnumerable<Personal> query;
            int inicio = (num_pagina - 1) * reg_por_pagina;
            if (campo == "")
            {
                query = from c in Cliente select c;
            }
            else
            {
                query = from c in Cliente where c.ID.StartsWith(campo) || c.Nombre.StartsWith(campo) 
                        || c.Apellido.StartsWith(campo) select c;
            }
            DataGridView.DataSource = query./*Skip(inicio).Take(reg_por_pagina).*/ToList();
            DataGridView.Columns[0].Visible = false;

            DataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            DataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        public void buscarProductoCliente(DataGridView DataGridView, int campo, int num_pagina, int reg_por_pagina)
        {
            IEnumerable<VentasClientes> query;
            int inicio = (num_pagina - 1) * reg_por_pagina;
            query = Ventasclientes.Where(v => v.IdCliente.Equals(campo)).ToList();
            DataGridView.DataSource = query.Skip(inicio).Take(reg_por_pagina).ToList();
            DataGridView.Columns[0].Visible = false;
            DataGridView.Columns[1].Visible = false;
            DataGridView.Columns[5].Visible = false;
            DataGridView.Columns[7].Visible = false;
            DataGridView.Columns[8].Visible = false;
            DataGridView.Columns[9].Visible = false;

            DataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            DataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        public void getClienteReporte(DataGridView dataGridView, int idCliente)
        {
            var query = from c in Cliente
                        join r in ReportesClientes on c.IdPersonal equals r.IdCliente
                        where c.IdPersonal == idCliente
                        select new
                        {
                            r.IdRegistro,
                            c.Nombre,
                            c.Apellido,
                            r.SaldoActual,
                            r.FechaActual,
                            r.UltimoPago,
                            r.FechaPago
                        };
            dataGridView.DataSource = query.ToList();
            dataGridView.Columns[0].Visible = false;
        }

        public void updateCliente(string id, string nombre, string apellido, string telefono, 
            int idCliente)
        {
            Cliente.Where(c => c.IdPersonal == idCliente)
                .Set(c => c.ID, id)
                .Set(c => c.Nombre, nombre)
                .Set(c => c.Apellido, apellido)
                .Set(c => c.Telefono, telefono)
                .Update();
            reporte = getReporte(idCliente);
            /*ReportesClientes.Where(r => r.IdRegistro == reporte[0].IdRegistro)
                .Set(r => r.IdCliente, reporte[0].IdCliente)
                .Set(r => r.SaldoActual, reporte[0].SaldoActual)
                .Set(r => r.FechaActual, reporte[0].FechaActual)
                .Set(r => r.UltimoPago, reporte[0].UltimoPago)
                .Set(r => r.FechaPago, reporte[0].FechaPago)
                .Set(r => r.ID, id)
                .Update();*/
        }

        public List<ReportesClientes> getReporte(int idCliente)
        {
            return ReportesClientes.Where(r => r.IdCliente == idCliente).ToList();
        }

        public void borrarCliente(int idCliente, int idRegistro)
        {
            /*ReportesClientes.Where(r => r.IdRegistro == idRegistro).Delete();*/
            Cliente.Where(c => c.IdPersonal == idCliente).Delete();
        }

        public void actualizaRep(string deudaActual, string ultimoPago, int idCliente, string usuario)
        { 
            string fecha = System.DateTime.Now.ToString("dd/MMM/yyy");
            decimal saldo = Convert.ToDecimal(deudaActual);
            var datos = abonos.Where(a => a.Usuario.Equals(usuario) && a.Fecha.Equals(fecha)).ToList();
            reporte = getReporte(idCliente);
            ReportesClientes.Where(r => r.IdRegistro == reporte[0].IdRegistro)
               .Set(r => r.IdCliente, reporte[0].IdCliente)
               .Set(r => r.SaldoActual, "$" + deudaActual)
               .Set(r => r.FechaActual, fecha)
               .Set(r => r.UltimoPago, "$" + ultimoPago)
               .Set(r => r.FechaPago, fecha)
               .Set(r => r.ID, reporte[0].ID)
               .Update();
            if (datos.Count == 0)
            {
                abonos.Value(a => a.Importe, ultimoPago)
                      .Value(a => a.Usuario, usuario)
                      .Value(a => a.Fecha, fecha)
                      .Insert();
            }
            else
            {
                decimal abono = Convert.ToDecimal(ultimoPago) + Convert.ToDecimal(datos[0].Importe);
                abonos.Where(a => a.Usuario.Equals(usuario) && a.Fecha.Equals(fecha))
                      .Set(a => a.Importe, abono.ToString())
                      .Update();
            }
            if (saldo == 0)
            {
                Ventasclientes.Where(v => v.IdCliente.Equals(idCliente)).Delete();
            }
        }
    }
}
