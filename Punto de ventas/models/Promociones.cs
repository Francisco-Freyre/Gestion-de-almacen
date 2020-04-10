using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Promociones
    {
        [PrimaryKey, Identity]

        public int IdPromocion { set; get; }

        public string Codigo { set; get; }

        public int CantidadProductos { set; get; }

        public int Precio { set; get; }

        public string Descripcion { set; get; }

    }
}
