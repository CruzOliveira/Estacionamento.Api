using Estacionamento.Application.DTOs.Vaga;
using Estacionamento.Application.Services;
using Estacionamento.Application.Validators;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enuns;
using Estacionamento.Tests.Fakes;

namespace Estacionamento.Tests.Services;

public class VagaServiceTests
{
    [Fact]
    public async Task Deve_Criar_Vaga_Valida()
    {
        var repositorio = new VagaRepositoryFake();
        var unitOfWork = new UnitOfWorkFake();
        var service = new VagaService(repositorio, new CriarVagaRequestValidator(), unitOfWork);

        await service.CriacaoAsync(new CriarVagaRequest
        {
            Numero = 10,
            Tipo = TipoVeiculo.Carro
        });

        var vaga = Assert.Single(repositorio.Vagas);
        Assert.Equal(10, vaga.Numero);
        Assert.Equal(StatusVaga.Disponivel, vaga.Status);
    }

    [Fact]
    public async Task Nao_Deve_Criar_Vaga_Com_Numero_Duplicado()
    {
        var repositorio = new VagaRepositoryFake();
        repositorio.Vagas.Add(new Vaga(10, TipoVeiculo.Carro));
        var unitOfWork = new UnitOfWorkFake();
        var service = new VagaService(repositorio, new CriarVagaRequestValidator(), unitOfWork);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.CriacaoAsync(new CriarVagaRequest
        {
            Numero = 10,
            Tipo = TipoVeiculo.Carro
        }));
    }
}
