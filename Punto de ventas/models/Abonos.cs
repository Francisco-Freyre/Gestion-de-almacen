using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Abonos
    {
        [PrimaryKey, Identity]

        public int IdAbono { set; get; }

        public string Importe { set; get; }

        public string Usuario { set; get; }

        public string Fecha { set; get; }
    }
}
