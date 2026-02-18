// PvHeaderGetAllHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Poliza.DTOs;
using Application.Features.Poliza.Mappers;
using Entities = Domain.Entities;
using Application.Features.Poliza.Queries;

namespace Application.Features.Poliza.Handlers;

public class PvHeaderGetAllHandler : IRequestHandler<PvHeaderGetAllQuery, IEnumerable<PvHeaderQueryResponseDto>>
{
    private readonly IPvHeaderRepository _repo;
    public PvHeaderGetAllHandler(IPvHeaderRepository repo) => _repo = repo;

    public async Task<IEnumerable<PvHeaderQueryResponseDto>> Handle(PvHeaderGetAllQuery request, CancellationToken ct)
        => (await _repo.GetAllAsync()).Select(entity => PvHeaderMapper.ToDto(entity));
}
