using Application.Features.Cliente.Commands;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Handlers;

public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand, int>
{
    private readonly IClienteRepository _repo;

    public UpdateClienteHandler(IClienteRepository repo)
        => _repo = repo;

    public async Task<int> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        var entity = ClienteMapper.ToEntity(request); // Commands → Entity
        return await _repo.UpdateAsync(entity, c => c.Nombre, c => c.Apellido, c => c.Email);
    }
}
