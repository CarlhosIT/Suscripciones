using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Middleware
{

    public static class LoggearRespuestasExtensions 
    {
        public static IApplicationBuilder UseLoggearRespuestas(this IApplicationBuilder app) 
        {
            return app.UseMiddleware<LoggearRespuestas>();
        }
    }


    public class LoggearRespuestas
    {
        private readonly RequestDelegate siguiente;

        public LoggearRespuestas(RequestDelegate siguiente,ILogger<LoggearRespuestas> logger )
        {
            this.siguiente = siguiente;
            Logger = logger;
        }

        public ILogger<LoggearRespuestas> Logger { get; }

        //Inkove o InvokeAsyng

        public async Task InvokeAsync(HttpContext contexto) 
        {
            using (var ms = new MemoryStream())
            {
                var cuerpoOriginalRespuesta = contexto.Response.Body;
                contexto.Response.Body = ms;

                await siguiente(contexto);

                ms.Seek(0, SeekOrigin.Begin);
                String respuesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(cuerpoOriginalRespuesta);
                contexto.Response.Body = cuerpoOriginalRespuesta;

                Logger.LogInformation(respuesta);

            }
        }
    }
}
