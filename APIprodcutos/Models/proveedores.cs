using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIprodcutos.Models
{
    public class proveedores
    {
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9\s-]+$", ErrorMessage = "El nombre del proveedor solo puede contener letras, números, espacios y guiones.")]
        public string Descripcion { get; set; }
    }
}