// ClienteUpdateCommand.cs
using MediatR;
namespace Application.Features.Cliente.Commands;

public record ClienteUpdateCommand(int Id, string Email) : IRequest<int>;
