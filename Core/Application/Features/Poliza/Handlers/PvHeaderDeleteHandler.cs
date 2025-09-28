// PvHeaderDeleteHandler.cs
using MediatR;
using Application.Features.Poliza.Commands;
using Domain.Interfaces;

namespace Application.Features.Poliza.Handlers;

public class PvHeaderDeleteHandler : IRequestHandler<PvHeaderDeleteCommand, bool>
{
    private readonly IPvHeaderRepository _repo;
    public PvHeaderDeleteHandler(IPvHeaderRepository repo) => _repo = repo;

    public async Task<bool> Handle(PvHeaderDeleteCommand request, CancellationToken cancellationToken)
        => (await _repo.DeleteByIdAsync(request.CodSuc, request.CodRamo, request.NroPol, request.NroEndoso)) > 0;
}
