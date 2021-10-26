using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using WebApiAutores.Validaciones;

namespace WebApiAutores.DTOs
{
    public class LibroDTO
    {

        public int Id { get; set; }

        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 200)]
        public String Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }

        //public List<ComentarioDTo> Comentarios { get; set; }



    }
}
