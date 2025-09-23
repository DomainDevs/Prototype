using Application.Features.Cliente.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Commands.Create;

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