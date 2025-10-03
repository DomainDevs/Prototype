// PvHeaderCreateHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Poliza.Commands;
using Application.Features.Poliza.Mappers;
using Entities = Domain.Entities;

namespace Application.Features.Poliza.Handlers;

public class PvHeaderCreateHandler : IRequestHandler<PvHeaderCreateCommand, int>
{
    private readonly IPvHeaderRepository _repo;
    public PvHeaderCreateHandler(IPvHeaderRepository repo) => _repo = repo;

    public async Task<int> Handle(PvHeaderCreateCommand request, CancellationToken ct)
    {
        var entity = PvHeaderMapper.ToEntity(request);
        return await _repo.InsertAsync(entity);
    }
}
