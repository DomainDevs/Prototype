using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces;

// Interfaz IClienteRepository (autogenerada)
public interface IClienteRepository
{
    Task<int> InsertAsync(Cliente entity);
    Task<int> UpdateAsync(Cliente entity, params Expression<Func<Cliente, object>>[] includeProperties);
    Task<IEnumerable<Cliente>> GetAllAsync(params Expression<Func<Cliente, object>>[]? selectProperties);
    Task<Cliente?> GetByIdAsync(int id, params Expression<Func<Cliente, object>>[]? selectProperties);
    Task<int> DeleteByIdAsync(int id);
}
