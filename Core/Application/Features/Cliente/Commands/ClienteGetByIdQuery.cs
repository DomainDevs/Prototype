// ClienteGetByIdQuery.cs
using MediatR;
using Application.Features.Cliente.DTOs;

namespace Application.Features.Cliente.Queries;

public record ClienteGetByIdQuery(int Id) : IRequest<ClienteQueryResponseDto?>;
