using FluentValidation;
using Application.Features.Upload.Commands;

namespace Application.Features.Upload.Validator;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    private readonly string[] _allowedExtensions =
        { ".jpg", ".jpeg", ".png", ".webp", ".heif", ".heic" };

    public UploadFileCommandValidator()
    {
        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage("Debe especificarse un grupo de configuración válido.");

        RuleFor(x => x.FileName)
            .NotEmpty().WithMessage("El nombre del archivo es obligatorio.")
            .Must(name => !string.IsNullOrWhiteSpace(name))
                .WithMessage("Nombre inválido.")
            .Must(HaveValidImageExtension)
                .WithMessage($"Extensión no permitida. Solo se admiten: {string.Join(", ", _allowedExtensions)}")
            .Must(name => !name.Contains("..") &&
                           !name.Contains("/") &&
                           !name.Contains("\\"))
                .WithMessage("Nombre de archivo contiene caracteres no válidos.");

        RuleFor(x => x.Content)
            .NotNull().WithMessage("El contenido del archivo no puede ser nulo.")
            .Must(c => c.CanRead).WithMessage("El archivo no se puede leer.");

        RuleFor(x => x.Size)
            .GreaterThan(0).WithMessage("El archivo no puede estar vacío.")
            .LessThanOrEqualTo(5_242_880)
                .WithMessage("La imagen supera el límite máximo permitido de 5MB.");

        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage("El tipo de contenido es obligatorio.")
            .Must(x => x.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                .WithMessage("El tipo de contenido debe ser una imagen válida.");
    }

    private bool HaveValidImageExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName)?.ToLower();
        return !string.IsNullOrWhiteSpace(extension) &&
               _allowedExtensions.Contains(extension);
    }
}