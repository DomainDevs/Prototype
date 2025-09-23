using Application.Features.Cliente.DTOs;
using MediatR;

namespace Application.Features.Cliente.Queries
{
    public record GetClienteByIdQuery(int Id) : IRequest<ClienteRequestDto>;
}