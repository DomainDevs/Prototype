using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Commands.Delete;

public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand, bool>
{
    private readonly IClienteRepository _repo;

    public DeleteClienteHandler(IClienteRepository repo)
        => _repo = repo;

    public async Task<bool> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        var rows = await _repo.DeleteByIdAsync(request.Id);
        return rows > 0;
    }
}