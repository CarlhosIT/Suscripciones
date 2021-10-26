using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.DTOs
{
    public class LimitarPeticionesConfiguracion
    {
        public int PeticionesPorDiaGratuito { get; set; }
        public string[] ListaBlancaRutas { get; set; }
    }
}
