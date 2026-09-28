using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Http.Json;
using Estacionamento.Application.DTOs.Cliente;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;

namespace Estacionamento.IntegrationTests.Infraestrutura
{
    public class ClientesApiTests : IClassFixture<SqlServerFixture>
    {
        private readonly SqlServerFixture _fixture;

        public ClientesApiTests(SqlServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Deve_Listar_Clientes_Com_Sucesso()
        {
            await using var factory = new ApiFactory(_fixture.ConnectionString);

            using var client = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("https://localhost"),
                    AllowAutoRedirect = false
                }
                );

            using var resposta = await client.GetAsync("/api/clientes");

            Assert.Equal(HttpStatusCode.OK, resposta.StatusCode);
        }
        [Fact]
        public async Task Deve_Cadastrar_Cliente_E_Consultar_Por_Id()
        {
            await using var factory = new ApiFactory(_fixture.ConnectionString);

            using var client = factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    BaseAddress = new Uri("https://localhost"),
                    AllowAutoRedirect = false
                }
                );

            var request = new CriarClienteRequest
            {
                Nome = "Cliente Teste",
                Documento = "87671015008",
            };

            using var cadastro = await client.PostAsJsonAsync("/api/clientes", request);

            Assert.Equal(HttpStatusCode.Created, cadastro.StatusCode);

            var cliente = await cadastro.Content.ReadFromJsonAsync<ClienteResponse>();

            Assert.NotNull(cliente);
            Assert.NotEqual(Guid.Empty, cliente.Id);

            using var consulta = await client.GetAsync($"/api/clientes/{cliente.Id}");

            Assert.Equal(HttpStatusCode.OK, consulta.StatusCode);

            var encontrado = await consulta.Content.ReadFromJsonAsync<ClienteResponse>();

            Assert.NotNull(encontrado);
            Assert.Equal(cliente.Id, encontrado.Id);
            Assert.Equal(request.Nome, encontrado.Nome);
            Assert.Equal(request.Documento, encontrado.Documento);
        }
        [Fact]
        public async Task Nao_Deve_Cadastrar_Documento_Duplicado()
        {
            await using var factory = new ApiFactory(_fixture.ConnectionString);

            using var client = factory.CreateClient(
             new WebApplicationFactoryClientOptions
             {
                 BaseAddress = new Uri("https://localhost"),
                 AllowAutoRedirect = false
             }
             );

            var request = new CriarClienteRequest
            {
                Nome = "Cliente Teste",
                Documento = "16744595059",
            };


            using var cadastro1 = await client.PostAsJsonAsync("/api/clientes", request);

            Assert.Equal(HttpStatusCode.Created, cadastro1.StatusCode);

            using var cadastro2 = await client.PostAsJsonAsync("/api/clientes", request);

            Assert.Equal(HttpStatusCode.BadRequest, cadastro2.StatusCode);
        }
        [Fact]
        public async Task Nao_Deve_Cadastrar_Cliente_Sem_Nome()
        {
            await using var factory = new ApiFactory(_fixture.ConnectionString);

            using var client = factory.CreateClient(
             new WebApplicationFactoryClientOptions
             {
                 BaseAddress = new Uri("https://localhost"),
                 AllowAutoRedirect = false
             }
             );

            var request = new CriarClienteRequest
            {
                Nome = "",
                Documento = "76975575096",
            };

            using var cadastro = await client.PostAsJsonAsync("/api/clientes", request);

            Assert.Equal(HttpStatusCode.BadRequest, cadastro.StatusCode);


        }
        [Fact]
        public async Task Nao_Deve_Encontrar_Cliente_Com_Id_Inexistente()
        {
            await using var factory = new ApiFactory(_fixture.ConnectionString);

            using var client = factory.CreateClient(
             new WebApplicationFactoryClientOptions
             {
                 BaseAddress = new Uri("https://localhost"),
                 AllowAutoRedirect = false
             }
             );

            var idInexistente = Guid.NewGuid();

            using var consulta = await client.GetAsync($"/api/clientes/{idInexistente}");
            Assert.Equal(HttpStatusCode.NotFound, consulta.StatusCode);
        }

        [Fact]
        public async Task Deve_Excluir_Cliente_Cadastrado()
        {
            await using var factory = new ApiFactory(_fixture.ConnectionString);

            using var client = factory.CreateClient(
             new WebApplicationFactoryClientOptions
             {
                 BaseAddress = new Uri("https://localhost"),
                 AllowAutoRedirect = false
             }
             );

            using var cadastro = await client.PostAsJsonAsync("/api/clientes", new CriarClienteRequest
            {
                Nome = "Cliente Teste",
                Documento = "12345678901",
            });

            var cliente = await cadastro.Content.ReadFromJsonAsync<ClienteResponse>();

            using var exclusao = await client.DeleteAsync($"/api/clientes/{cliente.Id}");

            using var consulta = await client.GetAsync($"/api/clientes/{cliente.Id}");

            Assert.Equal(HttpStatusCode.NotFound, consulta.StatusCode);

        }
    }
}
