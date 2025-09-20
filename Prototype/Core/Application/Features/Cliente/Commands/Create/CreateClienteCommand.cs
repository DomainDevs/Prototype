using MediatR;

namespace Application.Features.Cliente.Commands.Create;

public record CreateClienteCommand(string? Nombre = null, string? Apellido = null, string? Email = null) : IRequest<int>;