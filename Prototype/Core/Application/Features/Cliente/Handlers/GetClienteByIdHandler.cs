using Application.DTOs;
using Application.Features.Cliente.Queries;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Handlers
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
            var cliente = await _repo.GetAllAsync();
            var entity = cliente.FirstOrDefault(c => c.Id == request.Id);

            if (entity is null) return null;

            return ClienteMapper.ToDto(entity); //entity  → Dto

        }
    }
}
