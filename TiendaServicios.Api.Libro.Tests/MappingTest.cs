using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using TiendaServicio.Api.Libro.Aplicacion;
using TiendaServicio.Api.Libro.Modelo;
using TiendaServicio.Api.Libro.Persistencia;
using Xunit;
namespace TiendaServicios.Api.Libro.Tests
{
    public class MappingTest:Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();


        }
    }
}
