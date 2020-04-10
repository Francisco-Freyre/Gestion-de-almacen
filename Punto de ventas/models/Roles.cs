using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Roles
    {
        [PrimaryKey, Identity]

        public int IdRol { set; get; }

        public string Rol { set; get; }

    }
}
