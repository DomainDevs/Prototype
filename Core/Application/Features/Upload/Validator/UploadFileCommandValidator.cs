using FluentValidation;
using Application.Features.Upload.Commands;

namespace Application.Features.Upload.Validator;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage("Debe especificarse un grupo de configuración válido.");

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("El nombre del archivo es obligatorio.");

        RuleFor(x => x.Content)
            .NotNull().WithMessage("El contenido del archivo no puede ser nulo.")
            .Must(c => c.Length > 0).WithMessage("El archivo no puede estar vacío.");
    }
}