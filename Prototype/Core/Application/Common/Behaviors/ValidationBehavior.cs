using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

/// <summary>
/// Representa un comportamiento de pipeline para MediatR que ejecuta validaciones
/// utilizando FluentValidation antes de procesar el request.
/// </summary>
/// <typeparam name="TRequest">El tipo de request que implementa <see cref="IRequest{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">El tipo de respuesta esperado por el request.</typeparam>
public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ValidationBehavior{TRequest, TResponse}"/>.
    /// </summary>
    /// <param name="validators">Colección de validadores que aplican a <typeparamref name="TRequest"/>.</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Intercepta la ejecución de un request antes de que llegue a su handler,
    /// ejecutando todas las validaciones de FluentValidation configuradas.
    /// </summary>
    /// <param name="request">El request actual que se está procesando.</param>
    /// <param name="next">Delegate que invoca el siguiente paso en el pipeline (otro behavior o el handler final).</param>
    /// <param name="cancellationToken">Token de cancelación.</param>
    /// <returns>La respuesta del handler si las validaciones son exitosas; de lo contrario, se lanza una excepción.</returns>
    /// <exception cref="ValidationException">
    /// Se lanza si uno o más validadores encuentran errores en el request.
    /// </exception>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}
