// Application/Polizas/GetPolizaCompletaUseCase.cs
using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Application.UseCase.Emision;

public class GetPolizaCompletaUseCase
{
    private readonly IPolizaRepository _repository;

    public GetPolizaCompletaUseCase(IPolizaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Polizas?> ExecuteAsync(int idPv)
    {
        return await _repository.GetPolizaCompletaAsync(idPv);
    }
}