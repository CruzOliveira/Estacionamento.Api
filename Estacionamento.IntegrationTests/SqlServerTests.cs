using Estacionamento.Infrastructure.Data;
using Estacionamento.IntegrationTests.Infraestrutura;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estacionamento.IntegrationTests
{
    public class SqlServerTests : IClassFixture<SqlServerFixture>
    {
        private readonly SqlServerFixture _fixture;
        public SqlServerTests(SqlServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Deve_Criar_As_Tabelas_Do_Estacionamento()
        {
           var options = new DbContextOptionsBuilder<EstacionamentoDbContext>()
                .UseSqlServer(_fixture.ConnectionString)
                .Options;


            await using var context = new EstacionamentoDbContext(options);


            Assert.Empty(await context.Clientes.ToListAsync());
            Assert.Empty(await context.Veiculos.ToListAsync());
            Assert.Empty(await context.Vagas.ToListAsync());
            Assert.Empty(await context.Estadias.ToListAsync());
        }
    }
}
