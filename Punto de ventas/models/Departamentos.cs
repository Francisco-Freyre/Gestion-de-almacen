using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Departamentos
    {  
        [PrimaryKey, Identity]

        public int IdDpto { set; get; }

        public string Departamento { set; get; }
    }
}
