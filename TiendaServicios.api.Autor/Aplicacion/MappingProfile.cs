using System;
using AutoMapper;
using TiendaServicios.api.Autor.Modelo;

namespace TiendaServicios.api.Autor.Aplicacion
{
    public class MappingProfile : Profile
    {
            public MappingProfile()
        {

            CreateMap<AutorLibro, AutorDto>();
        }
        
    }
}
