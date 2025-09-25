// DeletePvHeaderHandler.cs
using MediatR;
using Domain.Interfaces;

namespace Application.Features.Poliza.Commands.Delete;

public class DeletePvHeaderHandler : IRequestHandler<DeletePvHeaderCommand, bool>
{
    private readonly IPvHeaderRepository _repo;
    public DeletePvHeaderHandler(IPvHeaderRepository repo) => _repo = repo;

    public async Task<bool> Handle(DeletePvHeaderCommand request, CancellationToken cancellationToken)
        => (await _repo.DeleteByIdAsync(request.CodSuc, request.CodRamo, request.NroPol, request.NroEndoso)) > 0;
}