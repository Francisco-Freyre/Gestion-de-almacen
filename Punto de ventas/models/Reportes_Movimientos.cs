using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.models
{
    public class Reportes_Movimientos
    {
        [PrimaryKey, Identity]

        public int IdReporteMovimiento { set; get; }

        public string Codigo { set; get; }

        public string Descripcion { set; get; }

        public int CostoAnterior { set; get; }

        public int PrecioVentaAnterior { set; get; }

        public int ExistenciaAnterior { set; get; }

        public string DepartamentoAnterior { set; get; }

        public string CategoriaAnterior { set; get; }

        public int CostoNuevo { set; get; }

        public int PrecioVentaNuevo { set; get; }

        public int ExistenciaNueva { set; get; }

        public string DepartementoNuevo { set; get; }

        public string CategoriaNuevo { set; get; }

        public string Fecha { set; get; }

        public string Cajero { set; get; }
    }
}
