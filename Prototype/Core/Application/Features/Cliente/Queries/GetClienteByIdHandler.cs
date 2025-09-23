using Application.Features.Cliente.DTOs;
using Application.Features.Cliente.Mappers;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Cliente.Queries;

public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, ClienteResponseDto?>
{
    private readonly IClienteRepository _repo;

    public GetClienteByIdHandler(IClienteRepository repo)
        => _repo = repo;

    public async Task<ClienteResponseDto?> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity is null)
            return null;

        return ClienteMapper.ToResponseDto(entity);
    }
}
