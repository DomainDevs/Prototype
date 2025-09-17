// Application/Clientes/Commands/UpdateClienteCommand.cs
using MediatR;

namespace Application.Clientes.Commands;

public record UpdateClienteCommand(int Id, string? Nombre = null, string? Apellido = null, string? Email = null) : IRequest<bool>;