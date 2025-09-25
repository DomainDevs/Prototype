// GetPvHeaderByIdHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Poliza.DTOs;
using Application.Features.Poliza.Mappers;
using Entities = Domain.Entities;

namespace Application.Features.Poliza.Queries;

public class GetPvHeaderByIdHandler : IRequestHandler<GetPvHeaderByIdQuery, PvHeaderResponseDto?>
{
    private readonly IPvHeaderRepository _repo;
    public GetPvHeaderByIdHandler(IPvHeaderRepository repo) => _repo = repo;

    public async Task<PvHeaderResponseDto?> Handle(GetPvHeaderByIdQuery request, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.CodSuc, request.CodRamo, request.NroPol, request.NroEndoso);
        if (entity == null) return null;
        return PvHeaderMapper.ToDto(entity);
    }
}