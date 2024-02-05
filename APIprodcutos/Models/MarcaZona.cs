using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIprodcutos.Models
{
    public class MarcaZona
    {
        public int IdMarca { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public string Zona { get; set; }
    }
}