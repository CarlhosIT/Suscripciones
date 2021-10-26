using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Filtros
{
    public class MyFiltro : IActionFilter
    {
        private readonly ILogger<MyFiltro> logger;

        public MyFiltro(ILogger<MyFiltro> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation("Antes de la accion");           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("Despues de la accion");
        }
    }
}
