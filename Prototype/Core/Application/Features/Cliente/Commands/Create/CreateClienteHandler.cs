// CreateClienteHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Cliente.Mappers;
using Entities = Domain.Entities;

namespace Application.Features.Cliente.Commands.Create;

public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, int>
{
    private readonly IClienteRepository _repo;
    public CreateClienteHandler(IClienteRepository repo) => _repo = repo;

    public async Task<int> Handle(CreateClienteCommand request, CancellationToken ct)
    {
        var entity = ClienteMapper.ToEntity(request);
        return await _repo.InsertAsync(entity);
    }
}