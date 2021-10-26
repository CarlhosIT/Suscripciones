using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.DTOs
{
    public class DatoHATEOAS
    {

        public String Enlace { get; private set; }
        public String Descripcion { get; private set; }
        public String Metodo { get; private set; }

        public DatoHATEOAS(String enlace, String descripcion,String metodo)
        {
            Enlace = enlace;
            Descripcion = descripcion;
            Metodo = metodo;
        }



    }
}
