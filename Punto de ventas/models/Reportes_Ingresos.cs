using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Reportes_Ingresos
    {
        [PrimaryKey, Identity]

        public int IdIngreso { set; get; }

        public string Amin { set; get; }

        public int Caja { set; get; }

        public string Ingreso { set; get; }

        public string Fecha { set; get; }

    }
}
