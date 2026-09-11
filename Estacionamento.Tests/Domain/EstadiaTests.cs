using Estacionamento.Domain.Entities;

namespace Estacionamento.Tests.Domain;

public class EstadiaTests
{
    [Fact]
    public void Deve_Finalizar_Estadia_Com_Valor()
    {
        var entrada = DateTime.UtcNow.AddHours(-2);
        var estadia = new Estadia(Guid.NewGuid(), Guid.NewGuid(), entrada);

        estadia.Finalizar(DateTime.UtcNow, 20m);

        Assert.NotNull(estadia.Saida);
        Assert.Equal(20m, estadia.Valor);
    }

    [Fact]
    public void Nao_Deve_Finalizar_Estadia_Duas_Vezes()
    {
        var estadia = new Estadia(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow.AddHours(-1));
        estadia.Finalizar(DateTime.UtcNow, 10m);

        Assert.Throws<InvalidOperationException>(() => estadia.Finalizar(DateTime.UtcNow, 10m));
    }

    [Fact]
    public void Nao_Deve_Aceitar_Saida_Anterior_A_Entrada()
    {
        var entrada = DateTime.UtcNow;
        var estadia = new Estadia(Guid.NewGuid(), Guid.NewGuid(), entrada);

        Assert.Throws<ArgumentException>(() => estadia.Finalizar(entrada.AddMinutes(-1), 10m));
    }
}
