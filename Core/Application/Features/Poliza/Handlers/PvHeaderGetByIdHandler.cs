// PvHeaderGetByIdHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Poliza.DTOs;
using Application.Features.Poliza.Mappers;
using Application.Features.Poliza.Queries;
using Entities = Domain.Entities;

namespace Application.Features.Poliza.Handlers;

public class PvHeaderGetByIdHandler : IRequestHandler<PvHeaderGetByIdQuery, PvHeaderQueryResponseDto?>
{
    private readonly IPvHeaderRepository _repo;
    public PvHeaderGetByIdHandler(IPvHeaderRepository repo) => _repo = repo;

    public async Task<PvHeaderQueryResponseDto?> Handle(PvHeaderGetByIdQuery request, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.CodSuc, request.CodRamo, request.NroPol, request.NroEndoso);
        if (entity == null) return null;
        return PvHeaderMapper.ToDto(entity);
    }
}
