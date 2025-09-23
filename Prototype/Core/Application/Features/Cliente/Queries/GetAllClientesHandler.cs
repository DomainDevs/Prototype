using Application.Features.Cliente.DTOs;
using Application.Features.Cliente.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Queries;

public class GetAllClientesHandler : IRequestHandler<GetAllClientesQuery, IEnumerable<ClienteResponseDto>>
{
    private readonly IClienteRepository _repo;

    public GetAllClientesHandler(IClienteRepository repo) => _repo = repo;

    public async Task<IEnumerable<ClienteResponseDto>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repo.GetAllAsync();
        if (entities is null) return Enumerable.Empty<ClienteResponseDto>();

        // Mapear entidades -> DTOs (usa el mapper generado)
        return entities.Select(ClienteMapper.ToDto).ToList();
    }
}
