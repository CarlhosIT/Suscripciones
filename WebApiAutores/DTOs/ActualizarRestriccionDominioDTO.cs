using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.DTOs
{
    public class ActualizarRestriccionDominioDTO
    {

        [Required]
        public string Dominio { get; set; }
    }
}
