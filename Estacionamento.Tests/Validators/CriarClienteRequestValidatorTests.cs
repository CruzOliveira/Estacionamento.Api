using Estacionamento.Application.DTOs.Cliente;
using Estacionamento.Application.Validators;
using FluentValidation;

namespace Estacionamento.Tests.Validators;

public class CriarClienteRequestValidatorTests
{
    private readonly CriarClienteRequestValidator _validator = new();

    [Fact]
    public void Deve_Aceitar_Cliente_Com_Dados_Validos()
    {
        var request = new CriarClienteRequest
        {
            Nome = "Maria da Silva",
            Documento = "52998224725"
        };

        var resultado = _validator.Validate(request);

        Assert.True(resultado.IsValid);
    }

    [Fact]
    public void Deve_Rejeitar_Cliente_Sem_Nome()
    {
        var request = new CriarClienteRequest
        {
            Nome = string.Empty,
            Documento = "52998224725"
        };

        var resultado = _validator.Validate(request);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.PropertyName == nameof(CriarClienteRequest.Nome));
    }

    [Fact]
    public void Deve_Rejeitar_Cliente_Com_Cpf_Invalido()
    {
        var request = new CriarClienteRequest
        {
            Nome = "Maria da Silva",
            Documento = "11111111111"
        };

        var resultado = _validator.Validate(request);

        Assert.False(resultado.IsValid);
        Assert.Contains(resultado.Errors, erro => erro.PropertyName == nameof(CriarClienteRequest.Documento));
    }
}
