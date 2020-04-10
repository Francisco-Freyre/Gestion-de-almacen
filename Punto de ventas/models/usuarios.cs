using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class usuarios
    {
        [PrimaryKey, Identity]

        public int IdUsuario { set; get; }

        public string Nombre { set; get; }

        public string Apellido { set; get; }

        public string Telefono { set; get; }

        public string Direccion { set; get; }

        public string Correo { set; get; }

        public string Usuario { set; get; }

        public string Contraseña { set; get; }

        public string Rol { set; get; }
    }
}
