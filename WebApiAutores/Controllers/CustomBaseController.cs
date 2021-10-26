
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Controllers
{
    public class CustomBaseController :ControllerBase
    {

        protected string ObtenerUsuarioId()
        {
           
            var usuarioClaim = HttpContext.User.Claims.Where(x => x.Type == "id").FirstOrDefault();
            var usuarioId = usuarioClaim.Value;
            return usuarioId;
        }
    }
}
