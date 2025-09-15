// Application/Features/Clientes/Handlers/CreateClienteHandler.cs
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Clientes.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Clientes.Handlers
{
    public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, int>
    {
        private readonly IClienteRepository _repo;

        public CreateClienteHandler(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email
            };

            var id = await _repo.InsertAsync(cliente);
            return id;
        }
    }
}
