using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIprodcutos.Models
{
    public class Presentacion
    {
        public int IdPresentacion { get; set; }

        [Required(ErrorMessage = "La descripción de la presentación es obligatoria.")]
        [RegularExpression(@"^[a-zA-Z0-9\s-]+$", ErrorMessage = "La descripción de la presentación solo puede contener letras, números, espacios y guiones.")]
        public string Descripcion { get; set; }
    }
}