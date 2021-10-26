using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.DTOs
{
    public class CrearRestriccionIPDTO
    {
        public int LlaveId { get; set; }
        [Required]
        public string IP { get; set; }
    }
}
