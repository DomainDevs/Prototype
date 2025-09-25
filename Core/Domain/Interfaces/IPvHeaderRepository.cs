using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces;

// Interfaz IPvHeaderRepository (autogenerada)
public interface IPvHeaderRepository
{
    Task<int> InsertAsync(PvHeader entity);
    Task<int> UpdateAsync(PvHeader entity, params Expression<Func<PvHeader, object>>[] includeProperties);
    Task<int> DeleteByIdAsync(int codSuc, int codRamo, long nroPol, int nroEndoso);
    Task<PvHeader?> GetByIdAsync(int codSuc, int codRamo, long nroPol, int nroEndoso);
    Task<IEnumerable<PvHeader>> GetAllAsync();
}