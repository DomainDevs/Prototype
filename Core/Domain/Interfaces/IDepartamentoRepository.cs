using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces;

// Interfaz IDepartamentoRepository (autogenerada)
public interface IDepartamentoRepository
{
    Task<int> InsertAsync(Departamento entity);
    Task<int> UpdateAsync(Departamento entity, params Expression<Func<Departamento, object>>[] includeProperties);
    Task<IEnumerable<Departamento>> GetAllAsync(params Expression<Func<Departamento, object>>[]? selectProperties);
    Task<Departamento?> GetByIdAsync(int id, params Expression<Func<Departamento, object>>[]? selectProperties);
    Task<int> DeleteByIdAsync(int id);
}