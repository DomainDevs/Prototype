using Application.DTOs;
using MediatR;

namespace Application.Features.Clientes.Queries
{
    public record GetClienteByIdQuery(int Id) : IRequest<ClienteRequestDto>;
}