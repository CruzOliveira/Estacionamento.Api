using Estacionamento.Application.DTOs.Estadia;
using Estacionamento.Application.Services;
using Estacionamento.Application.Validators;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enuns;
using Estacionamento.Tests.Fakes;

namespace Estacionamento.Tests.Services;

public class EstadiaServiceTests
{
    [Fact]
    public async Task Deve_Criar_Estadia_E_Ocupar_Vaga_Compativel()
    {
        var veiculoRepository = new VeiculoRepositoryFake();
        var vagaRepository = new VagaRepositoryFake();
        var estadiaRepository = new EstadiaRepositoryFake();
        var veiculo = new Veiculo("ABC1234", TipoVeiculo.Carro, Guid.NewGuid());
        var vaga = new Vaga(10, TipoVeiculo.Carro);
        veiculoRepository.Veiculos.Add(veiculo);
        vagaRepository.Vagas.Add(vaga);
        var service = CriarService(veiculoRepository, vagaRepository, estadiaRepository);

        await service.CriacaoAsync(new CriarEstadiaRequest { VeiculoId = veiculo.Id });

        Assert.Single(estadiaRepository.Estadias);
        Assert.Equal(vaga.Id, estadiaRepository.Estadias[0].VagaId);
        Assert.Equal(StatusVaga.Ocupada, vaga.Status);
    }

    [Fact]
    public async Task Nao_Deve_Criar_Duas_Estadias_Abertas_Para_O_Mesmo_Veiculo()
    {
        var veiculoRepository = new VeiculoRepositoryFake();
        var vagaRepository = new VagaRepositoryFake();
        var estadiaRepository = new EstadiaRepositoryFake();
        var veiculo = new Veiculo("ABC1234", TipoVeiculo.Carro, Guid.NewGuid());
        veiculoRepository.Veiculos.Add(veiculo);
        vagaRepository.Vagas.Add(new Vaga(10, TipoVeiculo.Carro));
        estadiaRepository.Estadias.Add(new Estadia(veiculo.Id, Guid.NewGuid(), DateTime.UtcNow));
        var service = CriarService(veiculoRepository, vagaRepository, estadiaRepository);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.CriacaoAsync(new CriarEstadiaRequest { VeiculoId = veiculo.Id }));
    }

    [Fact]
    public async Task Deve_Finalizar_Estadia_Liberar_Vaga_E_Calcular_Valor_Minimo()
    {
        var veiculoRepository = new VeiculoRepositoryFake();
        var vagaRepository = new VagaRepositoryFake();
        var estadiaRepository = new EstadiaRepositoryFake();
        var vaga = new Vaga(10, TipoVeiculo.Carro);
        vaga.Ocupar();
        var estadia = new Estadia(Guid.NewGuid(), vaga.Id, DateTime.UtcNow.AddMinutes(-30));
        vagaRepository.Vagas.Add(vaga);
        estadiaRepository.Estadias.Add(estadia);
        var service = CriarService(veiculoRepository, vagaRepository, estadiaRepository);

        await service.FinalizarAsync(estadia.Id);

        Assert.NotNull(estadia.Saida);
        Assert.Equal(10m, estadia.Valor);
        Assert.Equal(StatusVaga.Disponivel, vaga.Status);
    }

    private static EstadiaService CriarService(
        VeiculoRepositoryFake veiculoRepository,
        VagaRepositoryFake vagaRepository,
        EstadiaRepositoryFake estadiaRepository) =>
        new(
            veiculoRepository,
            vagaRepository,
            estadiaRepository,
            new CriarEstadiaRequestValidator(),
            new UnitOfWorkFake());
}
