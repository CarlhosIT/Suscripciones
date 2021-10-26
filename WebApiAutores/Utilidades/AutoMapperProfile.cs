using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;
using WebApiAutores.Entidades;


namespace WebApiAutores.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTO>();
            CreateMap<Autor, AutorDTOCOnLIbros>().ForMember(autorDTO => autorDTO.Libros, opciones => opciones.MapFrom(MapAutorLibros));
            CreateMap<LibroCreacionDTO, Libro>().ForMember(libro => libro.autoresLibros, opciones => opciones.MapFrom(MapAutoresLibros));
            CreateMap<Libro, LibroDTO>();
            CreateMap<Libro, LibroConAutoresDTO>().ForMember(LibroDTO=> LibroDTO.Autores,opciones=> opciones.MapFrom(MapLibroDTOAutores));
            CreateMap<LibroPatchDTO, Libro>().ReverseMap();
            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario,ComentarioDTo>();

            CreateMap<LlaveApi, LlaveDTO>();
            CreateMap<RestriccionDominio,RestriccionDominioDTO>();
            CreateMap<RestriccionIP, RestriccionIPDTO>();


        }

        private List<LibroDTO> MapAutorLibros(Autor autor,AutorDTO autorDTO)
        {
            var resultado = new List<LibroDTO>();

            if (autor.autoresLibros == null) { return resultado; }
            foreach (var autorlibro in autor.autoresLibros)
            {
                resultado.Add(new LibroDTO() { Id = autorlibro.LibroId, Titulo = autorlibro.libro.Titulo  });
            }
            return resultado;
        }

        private List<AutoresLibros> MapAutoresLibros(LibroCreacionDTO libroDTO, Libro libro) 
        {
            var resultado = new List<AutoresLibros>();

            if (libroDTO.AutoresId==null) { return resultado; }
            foreach (var autorId in libroDTO.AutoresId) 
            {
                resultado.Add(new AutoresLibros() { AutorId=autorId});
            }
            return resultado;

        }
        private List<AutorDTO> MapLibroDTOAutores(Libro libro, LibroDTO libroDTO)
        {
            var resultado = new List<AutorDTO>();
            if (libro.autoresLibros == null) { return resultado; }
            foreach (var autorlibro in libro.autoresLibros)
            {
                resultado.Add(new AutorDTO()
                {
                    Id = autorlibro.AutorId,
                    Nombre = autorlibro.autor.Nombre
                });
            }
            return resultado;
        }
    }
}
