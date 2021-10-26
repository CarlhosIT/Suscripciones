using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Entidades
{
    public class AutoresLibros
    {


        public int LibroId { get; set; }
        public int AutorId { get; set; }
        public int Orden { get; set; }
        public Libro libro { get; set; }
        public Autor autor { get; set; }

    }
}
