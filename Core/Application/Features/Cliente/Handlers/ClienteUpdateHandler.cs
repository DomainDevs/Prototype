// ClienteUpdateHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Cliente.Commands;
using Application.Features.Cliente.Mappers;
using Entities = Domain.Entities;

namespace Application.Features.Cliente.Handlers;

public class ClienteUpdateHandler : IRequestHandler<ClienteUpdateCommand, int>
{
    private readonly IClienteRepository _repo;
    public ClienteUpdateHandler(IClienteRepository repo) => _repo = repo;

    public async Task<int> Handle(ClienteUpdateCommand request, CancellationToken ct)
    {
        var entity = ClienteMapper.ToEntity(request);
        return await _repo.UpdateAsync(entity, c => c.Email); //Specify fields => ...UpdateAsync(entity, c => c.Field1, c => c.Field2);
    }
}