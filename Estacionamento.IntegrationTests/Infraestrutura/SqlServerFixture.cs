using System;
using System.Collections.Generic;
using System.Text;
using Testcontainers.MsSql;
using Estacionamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Estacionamento.IntegrationTests.Infraestrutura
{
    public class SqlServerFixture : IAsyncLifetime
    {
        private readonly MsSqlContainer _container =
        new MsSqlBuilder()
            .WithImage(
                "mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04")
            .Build();

        public string ConnectionString => new SqlConnectionStringBuilder(_container.GetConnectionString())
        {
            InitialCatalog = "EstacionamentoTests"
        }.ConnectionString;

        public async Task InitializeAsync()
        {
            await _container.StartAsync();

            var connectionString = new SqlConnectionStringBuilder(_container.GetConnectionString())
            {
                InitialCatalog = "EstacionamentoTests"
            }.ConnectionString;

            var options = new DbContextOptionsBuilder<EstacionamentoDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            await using var context = new EstacionamentoDbContext(options);

            await context.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await _container.DisposeAsync();
        }
    }
}
