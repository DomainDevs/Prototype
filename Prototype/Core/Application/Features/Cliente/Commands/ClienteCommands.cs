using MediatR;

namespace Application.Features.Cliente.Commands;

public record CreateClienteCommand(string? Nombre = null, string? Apellido = null, string? Email = null) : IRequest<int>;
public record UpdateClienteCommand(int Id, string Nombre, string Apellido, string? Email) : IRequest<int>;
public record DeleteClienteCommand(int Id) : IRequest<bool>;