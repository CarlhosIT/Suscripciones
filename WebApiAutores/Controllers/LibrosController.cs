using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;
using WebApiAutores.Filtros;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public IConfiguration Configuration { get; }

        public LibrosController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            Configuration = configuration;
        }

        [HttpGet]
        [ServiceFilter(typeof(MyFiltro))]
        public async Task<List<LibroDTO>> Get()
        {
            var libros = await context.Libros.ToListAsync();

            return mapper.Map<List<LibroDTO>>(libros);

        }

        [HttpGet("{id:int}", Name = "obtenerLibro")]
        public async Task<ActionResult<LibroConAutoresDTO>> Get(int id)
        {
            var libro = await context.Libros.Include(LibroBD => LibroBD.autoresLibros).
                ThenInclude(autorLibroDB => autorLibroDB.autor)
                .FirstOrDefaultAsync(LibroBD => LibroBD.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            libro.autoresLibros = libro.autoresLibros.OrderBy(x => x.Orden).ToList();
            return mapper.Map<LibroConAutoresDTO>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post(LibroCreacionDTO libroDTO)
        {
            if (libroDTO.AutoresId == null) { return BadRequest("No se puede crear libros sin autores"); }


            var autoresId = await context.Autores.Where(autorBD => libroDTO.AutoresId.Contains(autorBD.Id))
                .Select(x => x.Id).ToListAsync();
            if (libroDTO.AutoresId.Count != autoresId.Count)
            {
                return BadRequest("Errores en los autores de libros");
            }
            var libro = mapper.Map<Libro>(libroDTO);

            for (int i = 0; i < libroDTO.AutoresId.Count; i++)
            {
                libro.autoresLibros[i].Orden = i;
            }
            context.Add(libro);
            await context.SaveChangesAsync();
            var libroDT = mapper.Map<LibroDTO>(libro);
            return CreatedAtRoute("obtenerLibro", new { id = libro.Id }, libroDT);
        }

        [HttpPut("id:int")]
        public async Task<ActionResult> Put(int id, LibroCreacionDTO LibroDTO)
        {
            var libroDB = await context.Libros.Include(x => x.autoresLibros).FirstOrDefaultAsync(x => x.Id == id);
            if (libroDB == null) { return NotFound(); }

            libroDB = mapper.Map(LibroDTO, libroDB);
            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<LibroPatchDTO> patchDocument)
        {
            if (patchDocument == null) { return BadRequest(); }

            var libroDB = await context.Libros.FirstOrDefaultAsync(x => x.Id == id);
            if (libroDB == null) { return NotFound(); }


            var libroDTO = mapper.Map<LibroPatchDTO>(libroDB);
            patchDocument.ApplyTo(libroDTO, ModelState);

            var esValido = TryValidateModel(libroDTO);
            if (!esValido) { return BadRequest(ModelState); }

            mapper.Map(libroDTO, libroDB);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Libros.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Libro() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("confi")]
        public ActionResult<String> confi() 
        {
            return Configuration["Apellido"];
            //return Configuration["ConnectionStrings:DefaultConnection"];
        }

    }
}
