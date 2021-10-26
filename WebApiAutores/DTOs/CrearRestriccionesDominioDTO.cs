using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.DTOs
{
    public class CrearRestriccionesDominioDTO
    {
        public int LlaveId { get; set; }
        [Required]
        public string Dominio { get; set; }
    }
}
