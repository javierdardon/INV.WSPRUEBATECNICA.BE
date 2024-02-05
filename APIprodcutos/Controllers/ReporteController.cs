using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using iTextSharp.text;
using iTextSharp.text.pdf;
using APIprodcutos.Models;
using APIproductos.Data;
using System.Net.Http.Headers;
using System;
using APIprodcutos.Data;
using System.Data.SqlClient;

namespace APIprodcutos.Controllers
{
    public class ReporteController : ApiController
    {
        // Genera un reporte de todos los productos en formato PDF.
        [HttpGet]
        [Route("api/reporte/productos")]
        public HttpResponseMessage GetReporteProductos()
        {
            // Obtener lista de productos desde la base de datos.
            List<Productos> listaProductos = ProductoData.Listar();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Configurar el documento PDF con tamaño de página y márgenes.
                Document document = new Document(PageSize.A3.Rotate(), 10, 10, 10, 10); // Tamaño de página A4 en orientación horizontal.
                PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Configurar y agregar el título del reporte al documento.
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                Paragraph title = new Paragraph("Reporte de Productos", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);
                document.Add(Chunk.NEWLINE);

                // Crear una tabla para organizar la información de los productos.
                PdfPTable table = new PdfPTable(11) { WidthPercentage = 100 };
                float[] columnWidths = new float[] { 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1 };
                table.SetWidths(columnWidths);

                // Añadir cabeceras de columna con formato en negrita y color de fondo.
                BaseColor headerBackgroundColor = new BaseColor(2, 136, 209); // Color de fondo para el encabezado.
                Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                string[] headers = { "Producto", "Marca", "Presentación", "Proveedor", "Zona", "Código", "Descripción", "Precio", "Stock", "IVA", "Peso" };
                foreach (var header in headers)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(header, boldFont))
                    {
                        BackgroundColor = headerBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(headerCell);
                }

                // Alternar colores de fondo para las filas.
                BaseColor evenRowColor = new BaseColor(224, 224, 224); // Gris claro para las filas pares.
                BaseColor oddRowColor = new BaseColor(255, 255, 255); // Blanco para las filas impares.

                int rowIndex = 0;
                foreach (var producto in listaProductos)
                {
                    BaseColor rowColor = (rowIndex % 2 == 0) ? evenRowColor : oddRowColor;
                    AddCellWithBackground(table, producto.IdProducto.ToString(), rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.MarcaDescripcion, rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.PresentacionDescripcion, rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.ProveedorDescripcion, rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.ZonaDescripcion, rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.Codigo.ToString(), rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.DescripcionProducto, rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, $"${producto.Precio}", rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.Stock.ToString(), rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.Iva.ToString(), rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.Peso.ToString("0.00 Kg"), rowColor, Element.ALIGN_CENTER);
                    rowIndex++;
                }

                document.Add(table);
                document.Close();

                byte[] bytes = memoryStream.ToArray();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(bytes)
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "ReporteProductos.pdf"
                };

                return response;
            }
        }

        private void AddCellWithBackground(PdfPTable table, string text, BaseColor color, int horizontalAlignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text))
            {
                BackgroundColor = color,
                HorizontalAlignment = horizontalAlignment
            };
            table.AddCell(cell);
        }


        // Este método genera un reporte en PDF de productos filtrados por un proveedor específico.
        [HttpGet]
        [Route("api/reporte/productosporproveedor/{idProveedor}")]
        public HttpResponseMessage GetReporteProductosPorProveedor(int idProveedor)
        {
            List<Productos> productosPorProveedor = ReportesData.ListarPorProveedor(idProveedor);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
                PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                Paragraph title = new Paragraph($"Reporte de Productos del Proveedor {idProveedor}")
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(title);
                document.Add(Chunk.NEWLINE); // Agrega un espacio después del título si es necesario

                PdfPTable table = new PdfPTable(10) { WidthPercentage = 100 }; // 9 columnas para los datos descriptivos y el código
                string[] headers = { "Producto", "Marca", "Presentación", "Proveedor", "Zona", "Código", "Precio", "Stock", "IVA", "Peso" };
                float[] columnWidths = new float[] { 2f, 2f, 2f, 2f, 2f, 1f, 1f, 1f, 1f, 1f };
                table.SetWidths(columnWidths);

                BaseColor headerBackgroundColor = new BaseColor(2, 136, 209); // Azul por ejemplo
                Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);

                foreach (string header in headers)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(header, boldFont))
                    {
                        BackgroundColor = headerBackgroundColor,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(headerCell);
                }

                BaseColor evenRowColor = new BaseColor(224, 224, 224); // Gris claro para las filas pares
                BaseColor oddRowColor = new BaseColor(255, 255, 255); // Blanco para las filas impares

                for (int rowIndex = 0; rowIndex < productosPorProveedor.Count; rowIndex++)
                {
                    Productos producto = productosPorProveedor[rowIndex];
                    BaseColor rowColor = (rowIndex % 2 == 0) ? evenRowColor : oddRowColor;

                    AddCellWithBackground(table, producto.DescripcionProducto, rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.MarcaDescripcion, rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.PresentacionDescripcion, rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.ProveedorDescripcion, rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.ZonaDescripcion, rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.Codigo.ToString(), rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.Precio.ToString("C2"), rowColor, Element.ALIGN_CENTER); // Formato de moneda
                    AddCellWithBackground(table, producto.Stock.ToString(), rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.Iva.ToString(), rowColor, Element.ALIGN_CENTER);
                    AddCellWithBackground(table, producto.Peso.ToString("0.00"), rowColor, Element.ALIGN_CENTER); // Formato con dos decimales
                }

                document.Add(table);
                document.Close();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(memoryStream.ToArray())
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = $"ReporteProductosProveedor{idProveedor}.pdf"
                };

                return response;
            }
        }


        [HttpGet]
        [Route("api/reporte/topmarcasporzona/{idZona}")]
        public IHttpActionResult GetTopMarcasPorZona()
        {
            try
            {
                // Intenta obtener el top de marcas por zona llamando al método ObtenerTopMarcasPorZona de la clase ReportesData.
                // Este método se espera que devuelva una lista de objetos MarcaZona, que contienen información sobre las marcas más populares en una zona específica.
                var topMarcasPorZona = ReportesData.ObtenerTopMarcasPorZona();

                // Si se obtiene con éxito, devuelve un código de estado HTTP 200 (OK) junto con la lista de marcas.
                return Ok(topMarcasPorZona);
            }
            catch (Exception ex)
            {
                // En caso de cualquier excepción, captura el error y devuelve un código de estado HTTP 500 (Error Interno del Servidor), 
                // proporcionando detalles del error para diagnóstico y corrección. Esto ayuda a identificar problemas en tiempo de ejecución que podrían ocurrir al realizar la consulta.
                return InternalServerError(ex);
            }
        }


    }
}

