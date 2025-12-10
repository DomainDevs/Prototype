// ClienteDeleteCommand.cs
using MediatR;
namespace Application.Features.Cliente.Commands;

public record ClienteDeleteCommand(int Id) : IRequest<bool>;
