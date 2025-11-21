// PvHeaderDeleteValidator.cs
using FluentValidation;
using Application.Features.Poliza.Commands;

namespace Application.Features.Poliza.Validators;

public class PvHeaderDeleteValidator : AbstractValidator<PvHeaderDeleteCommand>
{
    public PvHeaderDeleteValidator()
    {
        RuleFor(x => x.CodSuc)
            .GreaterThan(0).WithMessage("El identificador CodSuc debe ser mayor que cero.");
        RuleFor(x => x.CodRamo)
            .GreaterThan(0).WithMessage("El identificador CodRamo debe ser mayor que cero.");
        RuleFor(x => x.NroPol)
            .GreaterThan(0).WithMessage("El identificador NroPol debe ser mayor que cero.");
        RuleFor(x => x.NroEndoso)
            .GreaterThan(0).WithMessage("El identificador NroEndoso debe ser mayor que cero.");
    }
}