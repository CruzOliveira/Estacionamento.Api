using Estacionamento.Application.DTOs.Vaga;
using FluentValidation;

namespace Estacionamento.Application.Validators;

public class CriarVagaRequestValidator : AbstractValidator<CriarVagaRequest>
{
    public CriarVagaRequestValidator()
    {
        RuleFor(vaga => vaga.Numero)
            .GreaterThan(0)
            .WithMessage("O número da vaga deve ser maior que zero.");

        RuleFor(vaga => vaga.Tipo)
            .IsInEnum()
            .WithMessage("O tipo de veículo informado é inválido.");
    }
}
