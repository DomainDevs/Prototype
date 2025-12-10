// ClienteCreateCommand.cs
using MediatR;
namespace Application.Features.Cliente.Commands;

public record ClienteCreateCommand(int Id, string Nombre, string Apellido, string Email, string Ciudad) : IRequest<int>;
