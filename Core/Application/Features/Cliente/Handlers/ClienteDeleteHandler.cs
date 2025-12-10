// ClienteDeleteHandler.cs
using MediatR;
using Application.Features.Cliente.Commands;
using Domain.Interfaces;

namespace Application.Features.Cliente.Handlers;

public class ClienteDeleteHandler : IRequestHandler<ClienteDeleteCommand, bool>
{
    private readonly IClienteRepository _repo;
    public ClienteDeleteHandler(IClienteRepository repo) => _repo = repo;

    public async Task<bool> Handle(ClienteDeleteCommand request, CancellationToken cancellationToken)
        => (await _repo.DeleteByIdAsync(request.Id)) > 0;
}