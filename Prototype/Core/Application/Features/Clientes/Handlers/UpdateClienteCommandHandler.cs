// Application/Clientes/Commands/UpdateClienteCommandHandler.cs
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Clientes.Commands;

public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, bool>
{
    private readonly IClienteRepository _repository;

    public UpdateClienteCommandHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente
        {
            Id = request.Id,
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Email = request.Email
        };

        var rows = await _repository.UpdateAsync(cliente);
        return rows > 0;
    }
}