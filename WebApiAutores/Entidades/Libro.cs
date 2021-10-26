using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El Libro necesita Titulo")]
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength:200)]
        public String Titulo { get; set; }

        public DateTime?  FechaPublicacion { get; set; }

        public List<Comentario> Comentarios { get; set; }

        public List<AutoresLibros> autoresLibros { get; set; }


    }
}
