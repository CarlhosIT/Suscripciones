using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAutores.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public String Contenido { get; set; }
        public int LibroId { get; set; }
        public Libro libro { get; set; }
        public String  UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
    }
}
