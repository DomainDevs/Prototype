// Persistence/Repositories/IPolizaRepository.cs
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPolizaRepository
    {
        Task<Polizas?> GetPolizaCompletaAsync(int idPv, int? codRiesgo = null);
    }
}