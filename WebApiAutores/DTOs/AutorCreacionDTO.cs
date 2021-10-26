using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Validaciones;

namespace WebApiAutores.DTOs
{
    public class AutorCreacionDTO
    { 
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [StringLength(maximumLength: 7, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres")]
    [PrimeraLetraMayuscula]
    public String Nombre { get; set; }
    }
}
