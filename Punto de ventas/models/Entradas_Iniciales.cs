using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Entradas_Iniciales
    {
        [PrimaryKey, Identity]

        public int Id { set; get; }

        public int Caja { set; get; }

        public string Ingreso { set; get; }

        public string Type { set; get; }

        public int IdUsuario { set; get; }

        public string Fecha { set; get; }
    }
}
