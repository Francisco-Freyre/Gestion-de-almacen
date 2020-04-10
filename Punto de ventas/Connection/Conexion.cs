using LinqToDB;
using LinqToDB.Data;
using Punto_de_ventas.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.Connection
{
    public class Conexion : DataConnection
    {
        public Conexion() : base("Abarrotera") { }

        public ITable<Personal> Cliente { get { return GetTable<Personal>(); } }

        public ITable<ReportesClientes> ReportesClientes { get { return GetTable<ReportesClientes>(); } }

        public ITable<usuarios> Usuario { get { return GetTable<usuarios>(); } }

        public ITable<Cajas> Cajas { get { return GetTable<Cajas>(); } }

        public ITable<cajas_registros> cajastemporal { get { return GetTable<cajas_registros>(); } }

        public ITable<Departamentos> Departamento { get { return GetTable<Departamentos>(); } }

        public ITable<Categorias> Categoria { get { return GetTable<Categorias>(); } }

        public ITable<Articulos> Producto { get { return GetTable<Articulos>(); } }

        public ITable<Cajas_Ingresos> CajasIngresos { get { return GetTable<Cajas_Ingresos>(); } }

        public ITable<Movimientos> Movimientos { get { return GetTable<Movimientos>(); } }

        public ITable<TempoArticulos> TempoVentas { get { return GetTable<TempoArticulos>(); } }

        public ITable<Ventas> Ventas { get { return GetTable<Ventas>(); } }

        public ITable<Creditos_Ventas> CreditosVentas { get { return GetTable<Creditos_Ventas>(); } }

        public ITable<Roles> roles { get { return GetTable<Roles>(); } }

        public ITable<Reportes_Ingresos> ReportesIngresos { get { return GetTable<Reportes_Ingresos>(); } }

        public ITable<Reportes_Movimientos> ReportesMoviminetos { get { return GetTable<Reportes_Movimientos>(); } }

        public ITable<Promociones> Promos { get { return GetTable<Promociones>(); } }

        public ITable<VentasClientes> Ventasclientes { get { return GetTable<VentasClientes>(); } }

        public ITable<Entradas_Iniciales> entradasIniciales { get { return GetTable<Entradas_Iniciales>(); } }

        public ITable<SalidasDinero> salidasdinero { get { return GetTable<SalidasDinero>(); } }

        public ITable<Promociones_Dos> PromosDos { get { return GetTable<Promociones_Dos>(); } }

        public ITable<Mermas> Mermas{ get { return GetTable<Mermas>(); } }

        public ITable<Pedidos> pedidos { get { return GetTable<Pedidos>(); } }

        public ITable<Abonos> abonos { get { return GetTable<Abonos>(); } }
    }
}
