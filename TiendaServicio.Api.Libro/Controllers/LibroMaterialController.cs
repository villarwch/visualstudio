﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicio.Api.Libro.Aplicacion;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaServicio.Api.Libro.Controllers
{


    [Route("api/[controller]")]
    [ApiController]

    public class LibroMaterialController : ControllerBase
    {

        private readonly IMediator _mediator;
        public LibroMaterialController (IMediator mediator)
        {

            _mediator = mediator;
        }



        [HttpPost]
        public async Task<ActionResult<Unit>> Crear (Nuevo.Ejecuta data)
        {

            return await _mediator.Send(data);

        }


        [HttpGet]
        public async Task<ActionResult<List<LibroMaterialDto>>> GetLibros()
        {
            return await _mediator.Send(new Consulta.Ejecuta());
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<LibroMaterialDto>> GetLibroUnico(Guid id)
        {
            return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibroId = id });


        }





    }
}
