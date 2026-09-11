using Estacionamento.Application.DTOs.Estadia;
using FluentValidation;

namespace Estacionamento.Application.Validators;

public class CriarEstadiaRequestValidator : AbstractValidator<CriarEstadiaRequest>
{
    public CriarEstadiaRequestValidator()
    {
        RuleFor(estadia => estadia.VeiculoId)
            .NotEmpty()
            .WithMessage("O veículo é obrigatório.");
    }
}
