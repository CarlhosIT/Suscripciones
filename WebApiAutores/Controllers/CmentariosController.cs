using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers {

    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")]
    public class CmentariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public UserManager<Usuario> UserManager { get; }

        public CmentariosController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> UserManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.UserManager = UserManager;
        }

        

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTo>>> Get(int libroId)
        {
            var comentarios = await context.Comentarios.Where(ComentarioBD => ComentarioBD.LibroId == libroId).ToListAsync();
            return mapper.Map<List<ComentarioDTo>>(comentarios);

        }

        [HttpGet("{id:int}", Name = "obtenerComentario")]
        public async Task<ActionResult<ComentarioDTo>> GetById(int id)
        {
            var comentario = await context.Comentarios.FirstOrDefaultAsync(comentarioBD => comentarioBD.Id == id);
            if (comentario == null) { return NotFound(); }

            return mapper.Map<ComentarioDTo>(comentario);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTO comentarioDTO)
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var Usuario = await UserManager.FindByEmailAsync(email);
            var existelibro = await context.Libros.AnyAsync(LibroDB => LibroDB.Id == libroId);
            if (!existelibro)
            {
                return NotFound();
            }

            var comentario = mapper.Map<Comentario>(comentarioDTO);
            comentario.LibroId = libroId;
            comentario.UsuarioID = Usuario.Id;
            context.Add(comentario);
            await context.SaveChangesAsync();

            var comentariodto = mapper.Map<ComentarioDTo>(comentario);

            return CreatedAtRoute("obtenerComentario", new { id = comentario.Id, libroId = libroId }, comentariodto);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int libroId, int id, ComentarioCreacionDTO comentarioDTO)
        {
            var existeLbro = await context.Libros.AnyAsync(LibroDB => LibroDB.Id == libroId);
            if (!existeLbro) { return NotFound("No se encontro el Libro"); }
            
            var existeComentario = await context.Comentarios.AnyAsync(ComntarioBD => ComntarioBD.Id == id);
            if (!existeComentario) { return NotFound("No se encontro el comentario"); }

            var comentario = mapper.Map<Comentario>(comentarioDTO);
            comentario.Id = id;
            comentario.LibroId = libroId;
            context.Update(comentario);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
