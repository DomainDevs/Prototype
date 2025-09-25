using Application.Features.Cliente.Commands.Create;
using FluentValidation;

namespace Application.Features.Cliente.Commands.Create;

public class CreateClienteValidator : AbstractValidator<CreateClienteCommand>
{
    public CreateClienteValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres");

        RuleFor(x => x.Apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio")
            .MaximumLength(100).WithMessage("El apellido no puede superar los 100 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El email no es válido");
    }
}
