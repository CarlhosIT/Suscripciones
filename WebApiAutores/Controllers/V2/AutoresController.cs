using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;
using WebApiAutores.Filtros;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers.V2
{
    [ApiController]
    [Route("api/v2/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ServiceScoped sscoped;
        private readonly ServiceSingletone ssingletone;
        private readonly ServiceTransient stransient;
        private readonly IMapper mapper;

        public IAuthorizationService AuthorizationService { get; }

        public AutoresController(ApplicationDbContext context, 
            ServiceScoped Sscoped, ServiceSingletone Ssingletone, ServiceTransient Stransient,
            IMapper mapper,IAuthorizationService authorizationService)
        {
            this.context = context;
            sscoped = Sscoped;
            ssingletone = Ssingletone;
            stransient = Stransient;
            this.mapper = mapper;
            AuthorizationService = authorizationService;
        }



        [HttpGet]
        [ServiceFilter(typeof(MyFiltro))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<List<AutorDTO>> Get()
        {
            var autores = await context.Autores.ToListAsync();


            var dtos = mapper.Map<List<AutorDTO>>(autores);

            dtos.ForEach(dto =>
                { GenerarEnlace(dto); dto.Nombre = dto.Nombre.ToUpper(); }

            );
             

            return dtos;
        }



        [HttpGet("guid")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(MyFiltro))]
        public ActionResult<List<Autor>> GetGuid()
        {
            return Ok(new { AutoresControllerScoped = sscoped.guid, AutoresControllerTrasient = stransient.guid,AutoresControllerSIngleton=ssingletone.guid  });

        }



        [HttpGet("{id:int}", Name = "obtenerAutorv2")]
        public async Task<ActionResult<AutorDTOCOnLIbros>> Get(int id)
        {
            var autor = await context.Autores.Include(autorBD => autorBD.autoresLibros)
                .ThenInclude(autorBD => autorBD.libro)
                .FirstOrDefaultAsync(AutorBD => AutorBD.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            var dto =mapper.Map<AutorDTOCOnLIbros>(autor);

        GenerarEnlace(dto);


            return dto;
        }


        private void GenerarEnlace(AutorDTO autordto) 
        {
            autordto.enlaces.Add(new DatoHATEOAS(enlace: Url.Link("autorPorNombre", new {nombre=autordto.Nombre }),
                descripcion: "autorPorNombre", metodo: "GET"));
            
                autordto.enlaces.Add(new DatoHATEOAS(enlace: Url.Link("editarAutor", new { Id = autordto.Id }),
                    descripcion: "editarAutor", metodo: "PUT"));
                autordto.enlaces.Add(new DatoHATEOAS(enlace: Url.Link("eliminarAutor", new { Id = autordto.Id }),
                   descripcion: "eliminarAutor", metodo: "DELETE"));

      



        }




        [HttpGet("{nombre}",Name ="autorPorNombrev2")]
        public async Task<ActionResult<List<AutorDTO>>> Get([FromRoute]String nombre)
        {
            var autores = await context.Autores.Where(AutorBD => AutorBD.Nombre.Contains(nombre)).ToListAsync();
            if (autores == null)
            {
                return NotFound();
            }
            return mapper.Map<List<AutorDTO>>(autores);
        }

        [HttpPost(Name ="crearAutorv2")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Post([FromBody]AutorCreacionDTO autorCreacion)
        {
            var ExisteAutorConMismoNombre = await context.Autores.AnyAsync(AutorBD=>AutorBD.Nombre== autorCreacion.Nombre);
            if (ExisteAutorConMismoNombre) 
            {
                return BadRequest($"Ya existe el autor con el mismo nombre {autorCreacion.Nombre}");
            }

            var autor = mapper.Map<Autor>(autorCreacion);
            context.Add(autor);
            await context.SaveChangesAsync();
            var autorDTO = mapper.Map<AutorDTO>(autor);
            return CreatedAtRoute("obtenerAutor", new { id=autor.Id},autorDTO);
        }



        
        [HttpPut("{id:int}",Name ="editarAutorv2")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Put(AutorCreacionDTO autorDTO, int id) 
        {
           
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            var autor = mapper.Map<Autor>(autorDTO);
            context.Update(autor);
            await context.SaveChangesAsync();
            return NoContent();

        }
        
        [HttpDelete("{id:int}",Name ="eliminarAutorv2")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy ="EsAdmin")]
        public async Task<ActionResult> Delete(int id) 
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe) 
            {
                return NotFound();
            }
            context.Remove(new Autor() {Id=id });
            await context.SaveChangesAsync();
            return Ok();
        }

        
    }
}
