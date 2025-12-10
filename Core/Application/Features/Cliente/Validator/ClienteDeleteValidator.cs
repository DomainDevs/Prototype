// ClienteDeleteValidator.cs
using FluentValidation;
using Application.Features.Cliente.Commands;

namespace Application.Features.Cliente.Validators;

public class ClienteDeleteValidator : AbstractValidator<ClienteDeleteCommand>
{
    public ClienteDeleteValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador Id debe ser mayor que cero.");
    }
}