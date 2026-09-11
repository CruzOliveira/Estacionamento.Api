using Estacionamento.Application.DTOs.Veiculo;
using Estacionamento.Application.Validators;
using Estacionamento.Domain.Enuns;
using FluentValidation;

namespace Estacionamento.Tests.Validators;

public class CriarVeiculoRequestValidatorTests
{
    private readonly CriarVeiculoRequestValidator _validator = new();

    [Theory]
    [InlineData("ABC1234")]
    [InlineData("ABC1D23")]
    public void Deve_Aceitar_Placas_Validas(string placa)
    {
        var request = new CriarVeiculoRequest
        {
            Placa = placa,
            Tipo = TipoVeiculo.Carro,
            ClienteId = Guid.NewGuid()
        };

        var resultado = _validator.Validate(request);

        Assert.True(resultado.IsValid);
    }

    [Fact]
    public void Deve_Rejeitar_Placa_Invalida()
    {
        var request = new CriarVeiculoRequest
        {
            Placa = "ABC-1234",
            Tipo = TipoVeiculo.Carro,
            ClienteId = Guid.NewGuid()
        };

        var resultado = _validator.Validate(request);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.PropertyName == nameof(CriarVeiculoRequest.Placa));
    }

    [Fact]
    public void Deve_Rejeitar_Veiculo_Sem_Cliente()
    {
        var request = new CriarVeiculoRequest
        {
            Placa = "ABC1234",
            Tipo = TipoVeiculo.Carro,
            ClienteId = Guid.Empty
        };

        var resultado = _validator.Validate(request);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.PropertyName == nameof(CriarVeiculoRequest.ClienteId));
    }
}
