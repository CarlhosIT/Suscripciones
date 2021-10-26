using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        private readonly IAuthorizationService authorizationService;

        public RootController(IAuthorizationService AuthorizationService)
        {
            authorizationService = AuthorizationService;
        }

        [HttpGet(Name = "ObtenerRoot")]
        public async  Task<ActionResult<IEnumerable<DatoHATEOAS>>> Obtener()
        {
            var DatosHateoas = new List<DatoHATEOAS>();
            var esAdmin = await authorizationService.AuthorizeAsync(User,"esAdmin");
 
            DatosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("ObtenerRoot", new { }) , descripcion:"self", metodo:"GET") );

            DatosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("obtenerAutor", new { }), descripcion: "optiene los Autores", metodo: "GET"));

            if (esAdmin.Succeeded)
            {
                DatosHateoas.Add(new DatoHATEOAS(enlace: Url.Link("crearAutor", new { }), descripcion: "Crea Autor", metodo: "POST")); 
            }

            return DatosHateoas;
                 
        }
    }
}
