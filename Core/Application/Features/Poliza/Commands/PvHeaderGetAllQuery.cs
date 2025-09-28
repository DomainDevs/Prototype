// PvHeaderGetAllQuery.cs
using MediatR;
using Application.Features.Poliza.DTOs;

namespace Application.Features.Poliza.Queries;

public record PvHeaderGetAllQuery() : IRequest<IEnumerable<PvHeaderQueryResponseDto>>;

