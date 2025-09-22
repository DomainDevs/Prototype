using Application.DTOs;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Queries;

public class GetAllClientesHandler : IRequestHandler<GetAllClientesQuery, IEnumerable<ClienteRequestDto>>
{
    private readonly IClienteRepository _repo;

    public GetAllClientesHandler(IClienteRepository repo) => _repo = repo;

    public async Task<IEnumerable<ClienteRequestDto>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repo.GetAllAsync();
        if (entities is null) return Enumerable.Empty<ClienteRequestDto>();

        // Mapear entidades -> DTOs (usa el mapper generado)
        return entities.Select(ClienteMapper.ToDto).ToList();
    }
}
