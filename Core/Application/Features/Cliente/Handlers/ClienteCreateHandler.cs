// ClienteCreateHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Cliente.Commands;
using Application.Features.Cliente.Mappers;
using Entities = Domain.Entities;

namespace Application.Features.Cliente.Handlers;

public class ClienteCreateHandler : IRequestHandler<ClienteCreateCommand, int>
{
    private readonly IClienteRepository _repo;
    public ClienteCreateHandler(IClienteRepository repo) => _repo = repo;

    public async Task<int> Handle(ClienteCreateCommand request, CancellationToken ct)
    {
        var entity = ClienteMapper.ToEntity(request);
        return await _repo.InsertAsync(entity);
    }
}