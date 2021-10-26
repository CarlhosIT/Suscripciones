using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Autor: IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:7,ErrorMessage ="El campo {0} no debe tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public String Nombre { get; set; }
        public List<AutoresLibros> autoresLibros { get; set; }

        [Range(0,90)]
        [NotMapped]
        public int Edad { get; set; }
        [NotMapped]
        public int Menor { get; set; }
        [NotMapped]
        public int Mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Edad == 69) 
            {
                yield return new ValidationResult("La edad no puede ser 69",
                    new String[] { nameof(Edad) });
            }
            if (Mayor < Menor) 
            {
                yield return new ValidationResult("Mayor no puede ser menor que Menor", 
                    new String[] { nameof(Mayor) });
            }
        }
    }
}
