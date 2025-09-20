using MediatR;

namespace Application.Features.Clientes.Commands;

public record CreateClienteCommand(string? Nombre = null, string? Apellido = null, string? Email = null) : IRequest<int>;