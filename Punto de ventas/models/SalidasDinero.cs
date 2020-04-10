using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class SalidasDinero
    {
        [PrimaryKey, Identity]

        public int IdSalida { set; get; }

        public string Monto { set; get; }

        public string Motivo { set; get; }

        public int IdUsuario { set; get; }

        public string Fecha { set; get; }
    }
}
