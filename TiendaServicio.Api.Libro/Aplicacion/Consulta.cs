﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;

namespace TiendaServicio.Api.Libro.Aplicacion
{
    public class Consulta
    {

        public class Ejecuta : IRequest<List<LibroMaterialDto>> {



            public Ejecuta() { }
        };

        


        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDto>>
        {

            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador ( ContextoLibreria contexto, IMapper mapper)
            {

                _contexto = contexto;
                _mapper = mapper;

            }


            public async Task<List<LibroMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {


                var libros = await _contexto.LibreriaMaterial.ToListAsync();

                var librosDto = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDto>>(libros);

                return librosDto;







            }
        }
    }
}
