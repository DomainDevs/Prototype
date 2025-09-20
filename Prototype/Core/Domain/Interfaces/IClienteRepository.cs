using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IClienteRepository
{
    Task<int> InsertAsync(Cliente entity);
    Task<int> UpdateAsync(Cliente entity, params System.Linq.Expressions.Expression<Func<Cliente, object>>[] includeProperties);
    Task<int> DeleteAsync(Cliente entity);
    Task<Cliente?> GetByIdAsync(Cliente entity);
    Task<IEnumerable<Cliente>> GetAllAsync();
}
