// ClienteUpdateValidator.cs
using FluentValidation;
using Application.Features.Cliente.Commands;

namespace Application.Features.Cliente.Validators;

public class ClienteUpdateValidator : AbstractValidator<ClienteUpdateCommand>
{
    public ClienteUpdateValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El campo Email es obligatorio.");
        RuleFor(x => x.Email)
            .MaximumLength(50).WithMessage("El campo Email no puede exceder 50 caracteres.");
    }
}