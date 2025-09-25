// UpdatePvHeaderHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Poliza.Mappers;
using Entities = Domain.Entities;

namespace Application.Features.Poliza.Commands.Update;

public class UpdatePvHeaderHandler : IRequestHandler<UpdatePvHeaderCommand, int>
{
    private readonly IPvHeaderRepository _repo;
    public UpdatePvHeaderHandler(IPvHeaderRepository repo) => _repo = repo;

    public async Task<int> Handle(UpdatePvHeaderCommand request, CancellationToken ct)
    {
        var entity = PvHeaderMapper.ToEntity(request);
        return await _repo.UpdateAsync(entity, c => c.Prima, c => c.SumaAseg);
    }
}