using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIprodcutos.Models
{
    public class Zonas
    {
        public int IdZona { get; set; }

        [Required(ErrorMessage = "El nombre de la zona es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9\s-]+$", ErrorMessage = "El nombre de la zona solo puede contener letras, números, espacios y guiones.")]
        public string Descripcion { get; set; }
    }
}