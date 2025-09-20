using Application.Features.Clientes.Commands;
using Application.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Clientes.Handlers;

public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand, int>
{
    private readonly IClienteRepository _repo;

    public UpdateClienteHandler(IClienteRepository repo)
        => _repo = repo;

    public async Task<int> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        // Convertimos directamente el record UpdateClienteCommand a la entidad
        var entity = new Cliente
        {
            Id = request.Id,
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Email = request.Email
        };

        // Actualizamos solo los campos que se desean
        return await _repo.UpdateAsync(entity, c => c.Nombre, c => c.Apellido, c => c.Email);
    }
}
