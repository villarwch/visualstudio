using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.Autor.Modelo;
using TiendaServicios.api.Autor.Persistencia;
using AutoMapper;

namespace TiendaServicios.api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {

            public string AutorGuid { get; set; }

        }


        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {

            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;


            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }





            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _contexto.AutorLibro.Where(x => x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();


                if (autor == null)
                {
                    throw new Exception("No se encontró el autor");
                }

                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autor);

                return autorDto;
            }

            /*  public async Task<AutorLibro> Handle(AutorUnico request, CancellationToken cancellationToken)
              {
                  var autor = await _contexto.AutorLibro.Where(x => x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();


                  if (autor == null)
                  {
                      throw new Exception("No se encontró el autor");
                  }


              }*/
        }



    }
}
