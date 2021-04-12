﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using TiendaServicios.api.Autor.Modelo;
using TiendaServicios.api.Autor.Persistencia;

namespace TiendaServicios.api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }


        }


        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {

            public EjecutaValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }

        }


        public class Manejador : IRequestHandler<Ejecuta>
        {


            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {

                _contexto = contexto;
            }


            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    FechaNacimiento = request.FechaNacimiento,
                    Apellido = request.Apellido,
                    AutorLibroGuid =  Convert.ToString(Guid.NewGuid() )
                };

                _contexto.AutorLibro.Add(autorLibro);

                var valor = await _contexto.SaveChangesAsync();

                if (valor > 0)
                {
                    return Unit.Value;

                }

                throw new Exception("No se pudo insertar autor de libro");






            }
        }








    }
}
