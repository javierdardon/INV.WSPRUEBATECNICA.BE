using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIprodcutos.Models
{
    public class Marcas
    {
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "El nombre de la marca es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9\s-]+$", ErrorMessage = "El nombre de la marca solo puede contener letras, números, espacios y guiones.")]

        public string Descripcion { get; set; }
    }
}