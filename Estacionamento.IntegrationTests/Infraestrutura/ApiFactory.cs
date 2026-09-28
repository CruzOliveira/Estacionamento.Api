using Estacionamento.API.Controllers;
using Estacionamento.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Estacionamento.IntegrationTests.Infraestrutura
{
    public class ApiFactory : WebApplicationFactory<EstadiasController>
    {
        private readonly string _connectionString;

        public ApiFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<EstacionamentoDbContext>>();
                services.RemoveAll<EstacionamentoDbContext>();

                services.AddDbContext<EstacionamentoDbContext>(options =>
                    options.UseSqlServer(_connectionString));
            });
        }
    }
}
