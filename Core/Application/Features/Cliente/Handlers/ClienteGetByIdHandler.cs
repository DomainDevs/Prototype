// ClienteGetByIdHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Cliente.DTOs;
using Application.Features.Cliente.Mappers;
using Entities = Domain.Entities;
using Application.Features.Cliente.Queries;

namespace Application.Features.Cliente.Handlers;

public class ClienteGetByIdHandler : IRequestHandler<ClienteGetByIdQuery, ClienteQueryResponseDto?>
{
    private readonly IClienteRepository _repo;
    public ClienteGetByIdHandler(IClienteRepository repo) => _repo = repo;

    public async Task<ClienteQueryResponseDto?> Handle(ClienteGetByIdQuery request, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.Id);
        if (entity == null) return null;
        return ClienteMapper.ToDto(entity);
    }
}