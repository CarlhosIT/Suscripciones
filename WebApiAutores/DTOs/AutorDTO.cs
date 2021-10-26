using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.DTOs
{
    public class AutorDTO : Recursos
    {
        public int Id{ get; set; }
        public String Nombre { get; set; }
        
    }
}
