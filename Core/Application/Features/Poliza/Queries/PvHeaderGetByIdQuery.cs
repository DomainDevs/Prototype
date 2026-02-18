// PvHeaderGetByIdQuery.cs
using MediatR;
using Application.Features.Poliza.DTOs;

namespace Application.Features.Poliza.Queries;

public record PvHeaderGetByIdQuery(int CodSuc, int CodRamo, long NroPol, int NroEndoso) : IRequest<PvHeaderQueryResponseDto?>;

