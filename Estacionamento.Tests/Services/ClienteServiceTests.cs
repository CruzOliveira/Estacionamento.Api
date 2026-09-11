using Estacionamento.Application.DTOs.Cliente;
using Estacionamento.Application.Services;
using Estacionamento.Application.Validators;
using Estacionamento.Domain.Entities;
using Estacionamento.Tests.Fakes;

namespace Estacionamento.Tests.Services;

public class ClienteServiceTests
{
    [Fact]
    public async Task Deve_Adicionar_Cliente_Valido()
    {
        var repositorio = new ClienteRepositoryFake();
        var unitOfWork = new UnitOfWorkFake();
        var service = new ClienteService(repositorio, new CriarClienteRequestValidator(), unitOfWork);

        var response = await service.AdicionarAsync(new CriarClienteRequest
        {
            Nome = "Maria da Silva",
            Documento = "52998224725"
        });

        Assert.Single(repositorio.Clientes);
        Assert.Equal("Maria da Silva", response.Nome);
    }

    [Fact]
    public async Task Nao_Deve_Adicionar_Cliente_Com_Documento_Duplicado()
    {
        var repositorio = new ClienteRepositoryFake();
        repositorio.Clientes.Add(new Cliente("Maria da Silva", "52998224725"));
        var unitOfWork = new UnitOfWorkFake();
        var service = new ClienteService(repositorio, new CriarClienteRequestValidator(), unitOfWork);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.AdicionarAsync(new CriarClienteRequest
        {
            Nome = "João da Silva",
            Documento = "52998224725"
        }));
    }
}
