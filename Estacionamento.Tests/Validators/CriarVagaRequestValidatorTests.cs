using Estacionamento.Application.DTOs.Vaga;
using Estacionamento.Application.Validators;
using Estacionamento.Domain.Enuns;
using FluentValidation;

namespace Estacionamento.Tests.Validators;

public class CriarVagaRequestValidatorTests
{
    private readonly CriarVagaRequestValidator _validator = new();

    [Fact]
    public void Deve_Aceitar_Vaga_Com_Dados_Validos()
    {
        var request = new CriarVagaRequest
        {
            Numero = 1,
            Tipo = TipoVeiculo.Moto
        };

        var resultado = _validator.Validate(request);

        Assert.True(resultado.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Deve_Rejeitar_Numero_De_Vaga_Invalido(int numero)
    {
        var request = new CriarVagaRequest
        {
            Numero = numero,
            Tipo = TipoVeiculo.Moto
        };

        var resultado = _validator.Validate(request);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.PropertyName == nameof(CriarVagaRequest.Numero));
    }
}
