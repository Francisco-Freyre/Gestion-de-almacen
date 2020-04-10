using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class cajas_registros
    {
        [PrimaryKey, Identity]

        public int IdCajaTempo { set; get; }

        public int IdUsuario { set; get; }

        public string Nombre { set; get; }

        public string Apellido { set; get; }

        public string Rol { set; get; }

        public int IdCaja { set; get; }

        public int Caja { set; get; }

        public bool Estado { set; get; }

        public string Hora { set; get; }

        public string Fecha { set; get; }
    }
}
