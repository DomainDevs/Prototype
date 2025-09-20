// Application/Features/Clientes/Handlers/GetClienteByIdHandler.cs
// Application/Features/Clientes/Handlers/GetClienteByIdHandler.cs
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Features.Clientes.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Clientes.Handlers
{
    public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, ClienteRequestDto?>
    {
        private readonly IClienteRepository _repo;

        public GetClienteByIdHandler(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<ClienteRequestDto?> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _repo.GetAllAsync();
            var entity = clientes.FirstOrDefault(c => c.Id == request.Id);

            if (entity is null) return null;

            return new ClienteRequestDto
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Email = entity.Email
            };
        }
    }
}
