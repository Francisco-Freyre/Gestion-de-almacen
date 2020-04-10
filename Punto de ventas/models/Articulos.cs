using LinqToDB.Mapping;
using Punto_de_ventas.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Articulos 
    {
        [PrimaryKey, Identity]

        public int IdArticulo { set; get; }

        public string Codigo { set; get; }

        public string Descripcion { set; get; }

        public int Existencia { set; get; }

        public decimal Precio { set; get; }

        
    }
}
