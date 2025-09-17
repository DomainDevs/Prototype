// Application/Clientes/Commands/UpdateClienteCommand.cs
using MediatR;

namespace Application.Clientes.Commands;

public record UpdateClienteCommand(int Id, string Nombre, string Apellido, string Email) : IRequest<bool>;