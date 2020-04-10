using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_ventas.modelsclass
{
    public class ExportData
    {
        private static SaveFileDialog fichero;
        private static Microsoft.Office.Interop.Excel.Application aplicacion;
        private static Workbook libros_trabajo;
        private static Worksheet hoja_trabajo;
        public static void exportarDataGridViewExcel(DataGridView grd, String[] title, int[] colum)
        {
            fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xls)|*.xls";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();
                hoja_trabajo = (Worksheet)libros_trabajo.Worksheets.get_Item(1);

                var fila = 1;
                var count = 1;
                for (int i = 0; i < grd.Rows.Count; i++)
                {
                    for (int j = 0; j < grd.Columns.Count; j++)
                    {
                        var valor = true;
                        for (int a = 0; a < colum.Length; a++)
                        {
                            if (j == colum[a])
                            {
                                valor = false;
                            }
                        }
                        if (valor)
                        {
                            hoja_trabajo.Cells[fila + 1, count] = grd.Rows[i].Cells[j].Value.ToString();
                            count++;
                        }
                    }
                    fila++;
                    count = 1;
                }
                var colums = 1;
                for (int i = 0; i < title.Length; i++)
                {
                    hoja_trabajo.Cells[1, colums] = title[i];
                    colums++;
                }
                libros_trabajo.SaveAs(fichero.FileName,XlFileFormat.xlWorkbookNormal);
                libros_trabajo.Close(true);
                aplicacion.Quit();
            }
            
        }
        public static void exportarDataGridViewPDF(DataGridView grd, String[] title, int[] colum)
        {
            fichero = new SaveFileDialog();
            fichero.Filter = "PDF (*.pdf)|*.pdf";
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                // Creamos el documento con el tamaño de página tradicional
                Document doc = new Document(PageSize.LETTER);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(fichero.FileName, FileMode.Create));
                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Sistema de ventas");
                doc.AddCreator("Abarrotera insurgentes");
                // Abrimos el archivo
                doc.Open();
                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                // Escribimos el encabezamiento en el documento
                doc.Add(new Paragraph("Productos en inventario"));
                doc.Add(Chunk.NEWLINE);
                // Creamos una tabla que contendrá el nombre, apellido y país
                // de nuestros visitante.
                PdfPTable tblProductos = new PdfPTable(title.Length);
                tblProductos.WidthPercentage = 100;
                // Configuramos el título de las columnas de la tabla
                for (int i = 0; i < title.Length; i++)
                {
                    PdfPCell columnas = new PdfPCell(new Phrase(title[i], _standardFont));
                    columnas.BorderWidth = 0;
                    columnas.BorderWidthBottom = 0.75f;
                    // Añadimos las celdas a la tabla
                    tblProductos.AddCell(columnas);
                }
                for (int i = 0; i < grd.Rows.Count; i++)
                {
                    for (int j = 0; j < grd.Columns.Count; j++)
                    {
                        var valor = true;
                        for (int a = 0; a < colum.Length; a++)
                        {
                            if (j == colum[a])
                            {
                                valor = false;
                            }
                        }
                        if (valor)
                        {
                            // Llenamos la tabla con información
                            PdfPCell producto = new PdfPCell(new Phrase(grd.Rows[i].Cells[j].Value.ToString(), _standardFont));
                            producto.BorderWidth = 0;
                            // Añadimos las celdas a la tabla
                            tblProductos.AddCell(producto);
                        }
                    }
                }
                doc.Add(tblProductos);

                doc.Close();
                writer.Close();
            }
        }
    }
}
