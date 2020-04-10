using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class ReportesClientes
    {
        [PrimaryKey, Identity]

        public int IdRegistro { set; get; }

        public int IdCliente { set; get; }

        public string SaldoActual { set; get; }
        public string FechaActual { set; get; }
        public string UltimoPago { set; get; }
        public string FechaPago { set; get; }
        public string ID { set; get; }
    }
}
