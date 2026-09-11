using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;

namespace Estacionamento.Tests.Fakes;


internal class UnitOfWorkFake : IUnitOfWork
{
    public int SaveChangesChamadas { get; private set; }

    public Task BeginTransactionAsync() => Task.CompletedTask;

    public Task SaveChangesAsync()
    {
        SaveChangesChamadas++;
        return Task.CompletedTask;
    }

    public Task CommitTransactionAsync() => Task.CompletedTask;

    public Task RollbackTransactionAsync() => Task.CompletedTask;
}

internal sealed class ClienteRepositoryFake : IClienteRepository
{
    public List<Cliente> Clientes { get; } = [];

    public Task AdicionarAsync(Cliente cliente)
    {
        Clientes.Add(cliente);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Cliente?>> ListarAsync() => Task.FromResult<IEnumerable<Cliente?>>(Clientes);
    public Task<Cliente?> ObterPorDocumentoAsynk(string documento) =>
        Task.FromResult(Clientes.FirstOrDefault(cliente => cliente.Documento == documento));
    public Task<Cliente?> ObterPorIdAsync(Guid id) =>
        Task.FromResult(Clientes.FirstOrDefault(cliente => cliente.Id == id));
    public Task RemoverAsync(Guid id)
    {
        Clientes.RemoveAll(cliente => cliente.Id == id);
        return Task.CompletedTask;
    }
}

internal sealed class VeiculoRepositoryFake : IVeiculoRepository
{
    public List<Veiculo> Veiculos { get; } = [];

    public Task CriacaoAsync(Veiculo veiculo)
    {
        Veiculos.Add(veiculo);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Veiculo?>> ListarAsync() => Task.FromResult<IEnumerable<Veiculo?>>(Veiculos);
    public Task<Veiculo?> ObterPorIdAsync(Guid id) =>
        Task.FromResult(Veiculos.FirstOrDefault(veiculo => veiculo.Id == id));
    public Task<Veiculo?> ObterPorPlacaAsync(string placa) =>
        Task.FromResult(Veiculos.FirstOrDefault(veiculo => veiculo.Placa == placa));
    public Task RemoverAsync(Guid id)
    {
        Veiculos.RemoveAll(veiculo => veiculo.Id == id);
        return Task.CompletedTask;
    }
}

internal sealed class VagaRepositoryFake : IVagaRepository
{
    public List<Vaga> Vagas { get; } = [];

    public Task AtualizarAsync(Vaga vaga) => Task.CompletedTask;
    public Task CriacaoAsync(Vaga vaga)
    {
        Vagas.Add(vaga);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Vaga?>> ListarAsync() => Task.FromResult<IEnumerable<Vaga?>>(Vagas);
    public Task<Vaga?> ObterPorIdAsync(Guid id) =>
        Task.FromResult(Vagas.FirstOrDefault(vaga => vaga.Id == id));
    public Task<Vaga?> ObterPorNumeroAsync(int numero) =>
        Task.FromResult(Vagas.FirstOrDefault(vaga => vaga.Numero == numero));
    public Task RemoverAsync(Guid id)
    {
        Vagas.RemoveAll(vaga => vaga.Id == id);
        return Task.CompletedTask;
    }
}

internal sealed class EstadiaRepositoryFake : IEstadiaRepository
{
    public List<Estadia> Estadias { get; } = [];

    public Task AtualizarAsync(Estadia estadia) => Task.CompletedTask;
    public Task CriacaoAsync(Estadia estadia)
    {
        Estadias.Add(estadia);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Estadia?>> ListarAsync() => Task.FromResult<IEnumerable<Estadia?>>(Estadias);
    public Task<Estadia?> ObterPorIdAsync(Guid id) =>
        Task.FromResult(Estadias.FirstOrDefault(estadia => estadia.Id == id));
}
