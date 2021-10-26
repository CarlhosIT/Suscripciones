using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiAutores.Servicios
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly IWebHostEnviroment env;
        private readonly String nombreArchivo = "Archivo1.txt";

        public EscribirEnArchivo(IWebHostEnviroment env)
        {
            this.env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        private void Escribir(String msj) 
        {
            // var ruta = $@"{env.ContentRootPath}";
        }

    }
}
