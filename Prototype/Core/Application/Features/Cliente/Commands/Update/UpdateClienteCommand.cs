using MediatR;

namespace Application.Features.Cliente.Commands.Update;
public record UpdateClienteCommand(int Id, string Nombre, string Apellido, string? Email) : IRequest<int>;