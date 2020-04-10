using Punto_de_ventas.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.modelsclass
{
    public class ClassModels
    {

        public static TextBoxEvent evento = new TextBoxEvent();
        public static Cliente cliente = new Cliente();
        public static Login login = new Login();
        public static Caja Caja = new Caja();
        public static Departamento dptocat = new Departamento();
        public static Producto Products = new Producto();
        public static Venta venta = new Venta();
        public static Imprimir imprimir = new Imprimir();
        public static Usuario usuario = new Usuario();
        public static Inventario invetario;
        public static ReportesDeMovimientos Reportes = new ReportesDeMovimientos();
        public static Corte Cortesitos = new Corte();
        public static Promocion promo = new Promocion();
        public static Merma merma = new Merma();
        //public static Mermas Merma = new Mermas();

        public static List<Personal> numCliente = new List<Personal>();
        public static List<Cajas> ejemplo_listCaja;
        public static List<usuarios> ejemplo_listUsuario;
        public static List<Articulos> products2;
        public static List<TempoArticulos> numTempoVentas;
    }
}
