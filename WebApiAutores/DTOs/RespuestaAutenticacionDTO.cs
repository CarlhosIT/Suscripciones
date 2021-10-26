using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.DTOs
{
    public class RespuestaAutenticacionDTO
    {
        public String Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
