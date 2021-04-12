using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.api.Autor.Aplicacion;
using TiendaServicios.api.Autor.Modelo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaServicios.api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {


        private readonly IMediator _mediator;

        public AutorController(IMediator mediator)
        {

            _mediator = mediator;

        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {


            return await _mediator.Send(data);

        }

        [HttpGet]
        //public async Task<ActionResult<List<AutorLibro>>> GetAutores() {
        public async Task<ActionResult<List<AutorDto>>> GetAutores() { 
        
            
            return await _mediator.Send(new Consulta.ListaAutor());

        }

        [HttpGet("{id}")]

         public async Task<ActionResult<AutorDto>> GetAutorLibro(String id)
        {

            return await _mediator.Send(new ConsultaFiltro.AutorUnico { AutorGuid = id });
        }



    }
}
