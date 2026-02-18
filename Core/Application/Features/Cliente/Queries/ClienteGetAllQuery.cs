// ClienteGetAllQuery.cs
using MediatR;
using Application.Features.Cliente.DTOs;

namespace Application.Features.Cliente.Queries;

public record ClienteGetAllQuery() : IRequest<IEnumerable<ClienteQueryResponseDto>>;
