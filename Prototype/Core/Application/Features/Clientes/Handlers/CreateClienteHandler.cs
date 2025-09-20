using Application.Features.Clientes.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, int>
{
    private readonly IClienteRepository _clienteRepository;

    public CreateClienteHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<int> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Email = request.Email
        };

        return await _clienteRepository.InsertAsync(cliente);
    }
}
