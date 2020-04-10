using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas.modelsclass
{
    public class Paginador
    {
        private DataGridView dataGridView;
        private Label label;
        private static int maxReg, pageSize = 100, pageCount, numPagi = 1;
        private int paginas, res;

        public Paginador(DataGridView dataGridView, Label label, int paginas, int res)
        {
            this.dataGridView = dataGridView;
            this.label = label;
            this.paginas = paginas;
            this.res = res;
            cargarDatos();
        }
        private void cargarDatos()
        {
            switch (paginas)
            {
                case 0:
                    if (res == 0)
                        numPagi = 1;
                    ClassModels.numTempoVentas = ClassModels.venta.getTempoVentas();
                    ClassModels.venta.buscarVentaTempo(dataGridView, 1, pageSize);
                    maxReg = ClassModels.numTempoVentas.Count();
                    break;
                case 1:
                    if (res == 0)
                        numPagi = 1;
                    ClassModels.numCliente = ClassModels.cliente.getClientes();
                    ClassModels.cliente.buscarCliente(dataGridView, "", 1, pageSize);
                    maxReg = ClassModels.numCliente.Count();
                    break;
                case 4:
                    if (res == 0)
                        numPagi = 1;
                    ClassModels.products2 = ClassModels.Products.getProductos();
                    ClassModels.Products.buscarProducto(dataGridView, "", 1, pageSize);
                    maxReg = ClassModels.products2.Count();
                    break;
                case 7:
                    //if (res == 0)
                    //    numPagi = 1;
                    //maxReg = ClassModels.invetario.getInventario().Count;
                    //ClassModels.invetario.getProducto("", 1, pageSize);
                    break;
                case 8:
                    //if (res == 0)
                    //    numPagi = 1;
                    //maxReg = ClassModels.invetario.buscarVentas("", 1, pageSize);
                    break;
            }
            pageCount = (maxReg / pageSize);

            if ((maxReg % pageSize) > 0)
            {
                pageCount += 1;
            }
            label.Text = "Paginas " + "1" + "/" + pageCount.ToString();
        }
        public void primero()
        {
            numPagi = 1;
            label.Text = "Paginas " + numPagi.ToString() + "/ " + pageCount.ToString();
            switch (paginas)
            {
                case 0:
                    ClassModels.venta.buscarVentaTempo(dataGridView, 1, pageSize);
                    break;
                case 1:
                    ClassModels.cliente.buscarCliente(dataGridView, "", 1, pageSize);
                    break;
                case 4:
                    ClassModels.Products.buscarProducto(dataGridView, "", 1, pageSize);
                    break;
                case 7:
                    //ClassModels.invetario.getProducto("", 1, pageSize);
                    break;
                case 8:
                    //ClassModels.invetario.buscarVentas("", 1, pageSize);
                    break;
            }
        }

        public void anterior()
        {
            if (numPagi > 1)
            {
                numPagi -= 1;
                label.Text = "Paginas " + numPagi.ToString() + "/ " + pageCount.ToString();
                switch (paginas)
                {
                    case 0:
                        ClassModels.venta.buscarVentaTempo(dataGridView, numPagi, pageSize);
                        break;
                    case 1:
                        ClassModels.cliente.buscarCliente(dataGridView, "", numPagi, pageSize);
                        break;
                    case 4:
                        ClassModels.Products.buscarProducto(dataGridView, "", numPagi, pageSize);
                        break;
                    case 7:
                        //ClassModels.invetario.getProducto("", numPagi, pageSize);
                        break;
                    case 8:
                        //ClassModels.invetario.buscarVentas("", numPagi, pageSize);
                        break;
                }
            }
        }
        public void siguiente()
        {
            if (numPagi == pageCount)
                numPagi -= 1;
            if (numPagi < pageCount)
            {
                numPagi += 1;
                label.Text = "Paginas " + numPagi.ToString() + "/ " + pageCount.ToString();
                switch (paginas)
                {
                    case 0:
                        ClassModels.venta.buscarVentaTempo(dataGridView, numPagi, pageSize);
                        break;
                    case 1:
                        ClassModels.cliente.buscarCliente(dataGridView, "", numPagi, pageSize);
                        break;
                    case 4:
                        ClassModels.Products.buscarProducto(dataGridView, "", numPagi, pageSize);
                        break;
                    case 7:
                        //ClassModels.invetario.getProducto("", numPagi, pageSize);
                        break;
                    case 8:
                        //ClassModels.invetario.buscarVentas("", numPagi, pageSize);
                        break;
                }
            }
        }
        public void ultimo()
        {
            numPagi = pageCount;
            label.Text = "Paginas " + numPagi.ToString() + "/ " + pageCount.ToString();
            switch (paginas)
            {
                case 0:
                    ClassModels.venta.buscarVentaTempo(dataGridView, numPagi, pageSize);
                    break;
                case 1:
                    ClassModels.cliente.buscarCliente(dataGridView, "", numPagi, pageSize);
                    break;
                case 4:
                    ClassModels.Products.buscarProducto(dataGridView, "", numPagi, pageSize);
                    break;
                case 7:
                    //ClassModels.invetario.getProducto("", numPagi, pageSize);
                    break;
                case 8:
                    //ClassModels.invetario.buscarVentas("", numPagi, pageSize);
                    break;
            }
        }
    }
}
