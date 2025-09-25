using MediatR;

namespace Application.Features.Cliente.Commands.Delete;
public record DeleteClienteCommand(int Id) : IRequest<bool>;