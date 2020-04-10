using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Pedidos
    {
        [PrimaryKey, Identity]

        public int IdPedido { set; get; }

        public string Descripcion { set; get; }

        public int Cantidad { set; get; }

        public string Fecha { set; get; }

        public string Proveedor { set; get; }

    }
}
