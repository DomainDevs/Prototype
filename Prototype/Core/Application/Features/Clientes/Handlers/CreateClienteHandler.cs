using Application.Features.Clientes.Commands;
using Application.Mappers;
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
        var cliente = ClienteMapper.ToEntity(request); // Commands → Entity
        return await _clienteRepository.InsertAsync(cliente);
    }
}
