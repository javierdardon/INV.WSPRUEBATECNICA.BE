using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIprodcutos.Models
{
    public class Productos
    {
        public int IdProducto { get; set; }

        public int IdMarca { get; set; }

        public int IdPresentacion { get; set; }

        public int IdProveedor { get; set; }

        public int IdZona { get; set; }

        public int Codigo { get; set; }

        public string DescripcionProducto { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public int Iva { get; set; }

        public decimal Peso { get; set; }
        public string MarcaDescripcion { get; set; }
        public string PresentacionDescripcion { get; set; }
        public string ProveedorDescripcion { get; set; }
        public string ZonaDescripcion { get; set; }
    }
}