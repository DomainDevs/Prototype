using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IClienteRepository
{
    bool AutoCommit { get; set; }

    Task<int> DeleteAsync(Cliente entity);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(Cliente entity);
    Task<int> InsertAsync(Cliente entity);
    Task<int> UpdateAsync(Cliente entity, Expression<Func<Cliente, object>>? includeProperties = null);
}