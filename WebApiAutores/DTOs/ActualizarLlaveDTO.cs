using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.DTOs
{
    public class ActualizarLlaveDTO
    {
        public int LlaveId { get; set; }
        public bool ActualizarLlave { get; set; }
        public bool Activa { get; set; }
    }
}
