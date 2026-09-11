using Estacionamento.Application.DTOs.Veiculo;
using Estacionamento.Application.Services;
using Estacionamento.Application.Validators;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enuns;
using Estacionamento.Tests.Fakes;

namespace Estacionamento.Tests.Services;

public class VeiculoServiceTests
{
    [Fact]
    public async Task Deve_Criar_Veiculo_Valido()
    {
        var repositorio = new VeiculoRepositoryFake();
        var unitOfWork = new UnitOfWorkFake();
        var service = new VeiculoService(repositorio, new CriarVeiculoRequestValidator(), unitOfWork);

        var response = await service.CriacaoAsync(new CriarVeiculoRequest
        {
            Placa = "ABC1234",
            Tipo = TipoVeiculo.Carro,
            ClienteId = Guid.NewGuid()
        });

        Assert.Single(repositorio.Veiculos);
        Assert.Equal("ABC1234", response.Placa);
    }

    [Fact]
    public async Task Nao_Deve_Criar_Veiculo_Com_Placa_Duplicada()
    {
        var repositorio = new VeiculoRepositoryFake();
        repositorio.Veiculos.Add(new Veiculo("ABC1234", TipoVeiculo.Carro, Guid.NewGuid()));
        var unitOfWork = new UnitOfWorkFake();
        var service = new VeiculoService(repositorio, new CriarVeiculoRequestValidator(), unitOfWork);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.CriacaoAsync(new CriarVeiculoRequest
        {
            Placa = "ABC1234",
            Tipo = TipoVeiculo.Carro,
            ClienteId = Guid.NewGuid()
        }));
    }
}
