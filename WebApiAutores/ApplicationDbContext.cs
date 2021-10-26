using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Entidades;

namespace WebApiAutores
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutoresLibros>().HasKey(al=> new {al.AutorId, al.LibroId });
            modelBuilder.Entity<Factura>()
                .Property(x => x.Monto).HasColumnType("decimal(18,2)");

        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; } 
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<AutoresLibros> AutoresLibros { get; set; }
        public DbSet<LlaveApi> LlaveApis { get; set; }
        public DbSet<Peticion> Peticiones { get; set; }
        public DbSet<RestriccionDominio> RestriccionesDominio { get; set; }
        public DbSet<RestriccionIP> RestriccionesIP { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaEmitida> FacturasEmitidas { get; set; }

    }
}
