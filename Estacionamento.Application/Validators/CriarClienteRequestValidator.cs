using Estacionamento.Application.DTOs.Cliente;
using FluentValidation;

namespace Estacionamento.Application.Validators;

public class CriarClienteRequestValidator : AbstractValidator<CriarClienteRequest>
{
    public CriarClienteRequestValidator()
    {
        RuleFor(cliente => cliente.Nome)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .MinimumLength(3)
            .WithMessage("O nome deve ter pelo menos 3 caracteres.")
            .MaximumLength(100)
            .WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(cliente => cliente.Documento)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("O documento é obrigatório.")
            .Matches(@"^\d{11}$")
            .WithMessage("O CPF deve conter exatamente 11 números.")
            .Must(SerCpfValido)
            .WithMessage("O CPF informado é inválido.");
    }

    private static bool SerCpfValido(string? cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf) || cpf.Distinct().Count() == 1)
        {
            return false;
        }

        var somaPrimeiroDigito = Enumerable.Range(0, 9)
            .Sum(indice => (cpf[indice] - '0') * (10 - indice));
        var primeiroDigito = CalcularDigitoVerificador(somaPrimeiroDigito);

        var somaSegundoDigito = Enumerable.Range(0, 10)
            .Sum(indice => (cpf[indice] - '0') * (11 - indice));
        var segundoDigito = CalcularDigitoVerificador(somaSegundoDigito);

        return cpf[9] - '0' == primeiroDigito && cpf[10] - '0' == segundoDigito;
    }

    private static int CalcularDigitoVerificador(int soma)
    {
        var resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
