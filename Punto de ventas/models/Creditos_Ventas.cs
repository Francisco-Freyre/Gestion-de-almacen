using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Creditos_Ventas
    {
        [PrimaryKey, Identity]

        public int IdCredito { set; get; }

        public string Total { set; get; }

        public string Pago { set; get; }

        public string Credito { set; get; }

        public int Dia { set; get; }

        public string Mes { set; get; }

        public string Año { set; get; }

        public string Fecha { set; get; }

        public string Cliente { set; get; }

        public int Caja { set; get; }

        public int IdUsuario { set; get; }
    }
}
