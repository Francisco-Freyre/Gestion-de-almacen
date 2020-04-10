using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Categorias
    {
        [PrimaryKey, Identity]

        public int IdCat { set; get; }

        public string Categoria { set; get; }

        public int IdDpto { set; get; }
    }
}
