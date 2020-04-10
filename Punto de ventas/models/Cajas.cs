using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Cajas
    {
        [PrimaryKey, Identity]

        public int IdCaja { set; get; }

        public int Caja { set; get; }

        public bool Estado { set; get; }
    }
}
