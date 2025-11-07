// PvHeaderUpdateHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Poliza.Commands;
using Application.Features.Poliza.Mappers;
using Entities = Domain.Entities;

namespace Application.Features.Poliza.Handlers;

public class PvHeaderUpdateHandler : IRequestHandler<PvHeaderUpdateCommand, int>
{
    private readonly IPvHeaderRepository _repo;
    public PvHeaderUpdateHandler(IPvHeaderRepository repo) => _repo = repo;

    public async Task<int> Handle(PvHeaderUpdateCommand request, CancellationToken ct)
    {
        var entity = PvHeaderMapper.ToEntity(request);
        return await _repo.UpdateAsync(entity, C => C.TxtDescription, C => C.Prima, C => C.SumaAseg); //Specify fields => ...UpdateAsync(entity, c => c.Field1, c => c.Field2);
    }
}