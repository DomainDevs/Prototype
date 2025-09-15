using MediatR;

namespace Application.Features.Clientes.Commands;

public record CreateClienteCommand(string Nombre, string Apellido, string Email) : IRequest<int>;