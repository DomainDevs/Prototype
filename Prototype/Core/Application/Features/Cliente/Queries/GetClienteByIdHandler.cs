using Application.DTOs;
using Application.Features.Cliente.Queries;
using Application.Mappers;
using Domain.Interfaces;
using MediatR;


namespace Application.Features.Cliente.Queries;

public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, ClienteRequestDto?>
{
    private readonly IClienteRepository _repo;

    public GetClienteByIdHandler(IClienteRepository repo)
    => _repo = repo;

    public async Task<ClienteRequestDto?> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        // Ahora usamos directamente el método del repositorio
        var entity = await _repo.GetByIdAsync(request.Id);

        if (entity is null)
            return null;

        return ClienteMapper.ToDto(entity); // Entity → Dto
    }
}
