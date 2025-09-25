// GetPvHeaderByIdQuery.cs
using MediatR;
using Application.Features.Poliza.DTOs;
using Application.Features.Poliza.Mappers;

namespace Application.Features.Poliza.Queries;

public record GetPvHeaderByIdQuery(int CodSuc, int CodRamo, long NroPol, int NroEndoso) : IRequest<PvHeaderResponseDto?>;