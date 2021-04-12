using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class AsyncEnumerator<T>: IAsyncEnumerator<T>


    {

        private readonly IEnumerator<T> enumerator;
        private IEnumerator<LibroMaterialDto> enumerator1;

        public T Current => enumerator.Current;


        public AsyncEnumerator(IEnumerator<T> enumerator) => this.enumerator = enumerator ?? throw new ArgumentNullException();

        public AsyncEnumerator(IEnumerator<LibroMaterialDto> enumerator1)
        {
            this.enumerator1 = enumerator1;
        }

        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return await Task.FromResult(enumerator.MoveNext());



        }
    }
}
