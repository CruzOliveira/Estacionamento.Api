using Estacionamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Estacionamento.Application.Interfaces;
using Estacionamento.Application.Services;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Infrastructure.Repositories;
using Estacionamento.API.Middleware;
using Estacionamento.Application.DTOs.Cliente;
using Estacionamento.Application.DTOs.Estadia;
using Estacionamento.Application.DTOs.Vaga;
using Estacionamento.Application.DTOs.Veiculo;
using Estacionamento.Application.Validators;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EstacionamentoDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IVagaRepository, VagaRepository>();
builder.Services.AddScoped<IEstadiaRepository, EstadiaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IVagaService, VagaService>();
builder.Services.AddScoped<IEstadiaService, EstadiaService>();

builder.Services.AddScoped<IValidator<CriarClienteRequest>, CriarClienteRequestValidator>();
builder.Services.AddScoped<IValidator<CriarVeiculoRequest>, CriarVeiculoRequestValidator>();
builder.Services.AddScoped<IValidator<CriarVagaRequest>, CriarVagaRequestValidator>();
builder.Services.AddScoped<IValidator<CriarEstadiaRequest>, CriarEstadiaRequestValidator>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<EstacionamentoDbContext>();

    await dbContext.Database.MigrateAsync();
}
app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
