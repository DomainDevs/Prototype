// ClienteCreateValidator.cs
using FluentValidation;
using Application.Features.Cliente.Commands;

namespace Application.Features.Cliente.Validators;

public class ClienteCreateValidator : AbstractValidator<ClienteCreateCommand>
{
    public ClienteCreateValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El campo Id es obligatorio.");
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0).WithMessage("El campo Id debe ser mayor o igual a 0.");
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El campo Nombre es obligatorio.");
        RuleFor(x => x.Nombre)
            .MaximumLength(50).WithMessage("El campo Nombre no puede exceder 50 caracteres.");
        RuleFor(x => x.Apellido)
            .MaximumLength(50).WithMessage("El campo Apellido no puede exceder 50 caracteres.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El campo Email es obligatorio.");
        RuleFor(x => x.Email)
            .MaximumLength(50).WithMessage("El campo Email no puede exceder 50 caracteres.");
        RuleFor(x => x.Ciudad)
            .MaximumLength(200).WithMessage("El campo Ciudad no puede exceder 200 caracteres.");
    }
}