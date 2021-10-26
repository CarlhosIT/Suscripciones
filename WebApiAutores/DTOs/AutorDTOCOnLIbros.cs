using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.DTOs
{
    public class AutorDTOCOnLIbros: AutorDTO
    {
        public List<LibroDTO> Libros { get; set; } 
    }
}
