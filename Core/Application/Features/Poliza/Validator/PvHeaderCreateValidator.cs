// PvHeaderCreateValidator.cs
using FluentValidation;
using Application.Features.Poliza.Commands;

namespace Application.Features.Poliza.Validators;

public class PvHeaderCreateValidator : AbstractValidator<PvHeaderCreateCommand>
{
    public PvHeaderCreateValidator()
    {
        RuleFor(x => x.IdPv)
            .NotEmpty().WithMessage("El campo IdPv es obligatorio.");
        RuleFor(x => x.IdPv)
            .GreaterThanOrEqualTo(0).WithMessage("El campo IdPv debe ser mayor o igual a 0.");
        RuleFor(x => x.CodSuc)
            .NotEmpty().WithMessage("El campo CodSuc es obligatorio.");
        RuleFor(x => x.CodSuc)
            .GreaterThanOrEqualTo(0).WithMessage("El campo CodSuc debe ser mayor o igual a 0.");
        RuleFor(x => x.CodRamo)
            .NotEmpty().WithMessage("El campo CodRamo es obligatorio.");
        RuleFor(x => x.CodRamo)
            .GreaterThanOrEqualTo(0).WithMessage("El campo CodRamo debe ser mayor o igual a 0.");
        RuleFor(x => x.NroPol)
            .NotEmpty().WithMessage("El campo NroPol es obligatorio.");
        RuleFor(x => x.NroPol)
            .GreaterThanOrEqualTo(0).WithMessage("El campo NroPol debe ser mayor o igual a 0.");
        RuleFor(x => x.NroEndoso)
            .NotEmpty().WithMessage("El campo NroEndoso es obligatorio.");
        RuleFor(x => x.NroEndoso)
            .GreaterThanOrEqualTo(0).WithMessage("El campo NroEndoso debe ser mayor o igual a 0.");
        RuleFor(x => x.TxtDescription)
            .MaximumLength(100).WithMessage("El campo TxtDescription no puede exceder 100 caracteres.");
    }
}