using MediatR;
using Application.Features.Cliente.DTOs;

namespace Application.Features.Cliente.Queries;

// Devuelve DTOs directamente para que el controller no tenga que mapear
public record GetAllClientesQuery() : IRequest<IEnumerable<ClienteResponseDto>>;

