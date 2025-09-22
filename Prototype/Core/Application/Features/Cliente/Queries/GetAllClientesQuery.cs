using MediatR;
using Application.DTOs;
using System.Collections.Generic;

namespace Application.Features.Cliente.Queries;

// Devuelve DTOs directamente para que el controller no tenga que mapear
public record GetAllClientesQuery() : IRequest<IEnumerable<ClienteRequestDto>>;

