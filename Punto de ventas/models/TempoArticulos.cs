using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class TempoArticulos
    {
        [PrimaryKey, Identity]

        public int IdTempo { set; get; }

        public string Codigo { set; get; }

        public string Descripcion { set; get; }

        public int Cantidad { set; get; }

        public int IdUsuario { set; get; }
    }
}
