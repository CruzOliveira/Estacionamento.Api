using Estacionamento.Application.DTOs.Estadia;
using Estacionamento.Application.Validators;
using FluentValidation;

namespace Estacionamento.Tests.Validators;

public class CriarEstadiaRequestValidatorTests
{
    private readonly CriarEstadiaRequestValidator _validator = new();

    [Fact]
    public void Deve_Aceitar_Estadia_Com_Veiculo()
    {
        var request = new CriarEstadiaRequest { VeiculoId = Guid.NewGuid() };

        var resultado = _validator.Validate(request);

        Assert.True(resultado.IsValid);
    }

    [Fact]
    public void Deve_Rejeitar_Estadia_Sem_Veiculo()
    {
        var request = new CriarEstadiaRequest { VeiculoId = Guid.Empty };

        var resultado = _validator.Validate(request);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.PropertyName == nameof(CriarEstadiaRequest.VeiculoId));
    }
}
