// CreatePvHeaderHandler.cs
using MediatR;
using Domain.Interfaces;
using Application.Features.Poliza.Mappers;
using Entities = Domain.Entities;

namespace Application.Features.Poliza.Commands.Create;

public class CreatePvHeaderHandler : IRequestHandler<CreatePvHeaderCommand, int>
{
    private readonly IPvHeaderRepository _repo;
    public CreatePvHeaderHandler(IPvHeaderRepository repo) => _repo = repo;

    public async Task<int> Handle(CreatePvHeaderCommand request, CancellationToken ct)
    {
        var entity = PvHeaderMapper.ToEntity(request);
        return await _repo.InsertAsync(entity);
    }
}