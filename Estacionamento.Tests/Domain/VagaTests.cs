using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enuns;

namespace Estacionamento.Tests.Domain;

public class VagaTests
{
    [Fact]
    public void Deve_Criar_Vaga_Disponivel()
    {
        var vaga = new Vaga(10, TipoVeiculo.Carro);

        Assert.Equal(StatusVaga.Disponivel, vaga.Status);
    }

    [Fact]
    public void Deve_Ocupar_E_Liberar_Vaga()
    {
        var vaga = new Vaga(10, TipoVeiculo.Carro);

        vaga.Ocupar();
        Assert.Equal(StatusVaga.Ocupada, vaga.Status);

        vaga.Liberar();
        Assert.Equal(StatusVaga.Disponivel, vaga.Status);
    }

    [Fact]
    public void Nao_Deve_Ocupar_Vaga_Ja_Ocupada()
    {
        var vaga = new Vaga(10, TipoVeiculo.Carro);
        vaga.Ocupar();

        Assert.Throws<InvalidOperationException>(vaga.Ocupar);
    }
}
