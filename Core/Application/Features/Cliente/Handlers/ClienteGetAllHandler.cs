// ClienteGetAllHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Cliente.DTOs;
using Application.Features.Cliente.Mappers;
using Entities = Domain.Entities;
using Application.Features.Cliente.Queries;

namespace Application.Features.Cliente.Handlers;

public class ClienteGetAllHandler : IRequestHandler<ClienteGetAllQuery, IEnumerable<ClienteQueryResponseDto>>
{
    private readonly IClienteRepository _repo;
    public ClienteGetAllHandler(IClienteRepository repo) => _repo = repo;

    public async Task<IEnumerable<ClienteQueryResponseDto>> Handle(ClienteGetAllQuery request, CancellationToken ct)
        => (await _repo.GetAllAsync()).Select(entity => ClienteMapper.ToDto(entity));
}