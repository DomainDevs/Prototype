// GetAllPvHeaderHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Poliza.DTOs;
using Application.Features.Poliza.Mappers;
using Entities = Domain.Entities;

namespace Application.Features.Poliza.Queries;

public class GetAllPvHeaderHandler : IRequestHandler<GetAllPvHeaderQuery, IEnumerable<PvHeaderResponseDto>>
{
    private readonly IPvHeaderRepository _repo;
    public GetAllPvHeaderHandler(IPvHeaderRepository repo) => _repo = repo;

    //public async Task<IEnumerable<PvHeaderResponseDto>> Handle(GetAllPvHeaderQuery request, CancellationToken ct)
    //    => (await _repo.GetAllAsync()).Select(entity => PvHeaderMapper.ToDto(entity));
    public async Task<IEnumerable<PvHeaderResponseDto>> Handle(GetAllPvHeaderQuery request, CancellationToken ct)
    {
        // Materializar IEnumerable en lista (indexable y debugeable)
        var entities = (await _repo.GetAllAsync()).ToList();

        // Aquí ya puedes usar el debugger: entities[0], entities[1], entities.Count, etc.
        var dtos = entities.Select(entity => PvHeaderMapper.ToDto(entity)).ToList();

        return dtos;
    }

}