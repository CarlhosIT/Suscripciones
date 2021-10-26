using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Entidades
{
    public class Usuario : IdentityUser
    {
        public bool MalaPaga { get; set; }
    }

    public class LlaveApi
    {
        public int Id { get; set; }
        public string Llave { get; set; }
        public TipoLlave TipoLlave { get; set; }
        public bool Activa { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<RestriccionDominio> RestriccionesDominio { get; set; }
        public List<RestriccionIP> RestriccionesIP { get; set; }
    }

    public enum TipoLlave
    {
        Gratuita = 1,
        Profesional = 2
    }

}
