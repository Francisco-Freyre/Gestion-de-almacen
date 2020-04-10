using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Movimientos
    {
        [PrimaryKey, Identity]

        public int IdMovimiento { set; get; }

        public string Codigo { set; get; }

        public string Descripcion { set; get; }

        public int Cantidad { set; get; }

        public string TipoMovimiento { set; get; }

        public string Fecha { set; get; }

        public string Hora { set; get; }

        public string Usuario { set; get; }
    }
}
