// GetAllPvHeaderQuery.cs
using MediatR;
using Application.Features.Poliza.DTOs;
using Application.Features.Poliza.Mappers;

namespace Application.Features.Poliza.Queries;

public record GetAllPvHeaderQuery() : IRequest<IEnumerable<PvHeaderResponseDto>>;