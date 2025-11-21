// PvHeaderUpdateValidator.cs
using FluentValidation;
using Application.Features.Poliza.Commands;

namespace Application.Features.Poliza.Validators;

public class PvHeaderUpdateValidator : AbstractValidator<PvHeaderUpdateCommand>
{
    public PvHeaderUpdateValidator()
    {
        RuleFor(x => x.IdPv)
            .NotEmpty().WithMessage("El campo IdPv es obligatorio.");
        RuleFor(x => x.CodSuc)
            .NotEmpty().WithMessage("El campo CodSuc es obligatorio.");
        RuleFor(x => x.CodRamo)
            .NotEmpty().WithMessage("El campo CodRamo es obligatorio.");
        RuleFor(x => x.NroPol)
            .NotEmpty().WithMessage("El campo NroPol es obligatorio.");
        RuleFor(x => x.NroEndoso)
            .NotEmpty().WithMessage("El campo NroEndoso es obligatorio.");
        RuleFor(x => x.TxtDescription)
            .MaximumLength(100).WithMessage("El campo TxtDescription no puede exceder 100 caracteres.");
    }
}