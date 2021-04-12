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
    public class LibrosServiceTest
    {

        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {

            //este metodo probaré llenar data de genfu

            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);

            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;


        }



        private Mock<ContextoLibreria> CrearContexto()
        {

            var dataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());


            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken())).Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));


            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));



            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;




        }

        [Fact]
        public async void GetLibroPorId()
        {

            var mockContexto = CrearContexto();

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libro);

            Assert.True(libro.LibreriaMaterialId == Guid.Empty);




        }



        [Fact]
        public async void GetLibros()
        {

            //System.Diagnostics.Debugger.Launch();
            //este test es de unitLibro


            //Que metodo dentro de mi microservice libro se esta encargajdo
            //de realizar consulta de libros en la bd?
            // 1.- emular instancia de entity fw core - contextolibreria


            //para emular la acciones y eventos de un objeto en ambiente de unit test
            //utilizamos objetos de tipo mock = representación objeto que actua como componente real

            //var mockContexto = new Mock<ContextoLibreria>();

            var mockContexto = CrearContexto();

            //2 emular al mapping imapper
            //var mockMapper = new Mock<IMapper>();


            var mapConfig = new MapperConfiguration(cfg =>
            {

                cfg.AddProfile(new MappingTest());

            });

            var mapper = mapConfig.CreateMapper();


            //3 instanciar clase manejador y pasarle como para los mocks que he creado

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object,mapper);


            Consulta.Ejecuta request = new Consulta.Ejecuta();


            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());


            Assert.True(lista.Any());







        }



        [Fact]

        public async void GuardarLibro()
        {

            System.Diagnostics.Debugger.Launch();

            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta();

            request.Titulo = "Libro de ms";
            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            var manejador = new Nuevo.Manejador(contexto);

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(libro != null);

        }


    }
}
