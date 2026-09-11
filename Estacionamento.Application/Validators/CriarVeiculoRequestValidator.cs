using Estacionamento.Application.DTOs.Veiculo;
using FluentValidation;

namespace Estacionamento.Application.Validators;

public class CriarVeiculoRequestValidator : AbstractValidator<CriarVeiculoRequest>
{
    public CriarVeiculoRequestValidator()
    {
        RuleFor(veiculo => veiculo.Placa)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("A placa é obrigatória.")
            .Matches("^[A-Za-z]{3}[0-9][A-Za-z0-9][0-9]{2}$")
            .WithMessage("A placa deve estar no formato ABC1234 ou ABC1D23.");

        RuleFor(veiculo => veiculo.Tipo)
            .IsInEnum()
            .WithMessage("O tipo de veículo informado é inválido.");

        RuleFor(veiculo => veiculo.ClienteId)
            .NotEmpty()
            .WithMessage("O cliente é obrigatório.");
    }
}
