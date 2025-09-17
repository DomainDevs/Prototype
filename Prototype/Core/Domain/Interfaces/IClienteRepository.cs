using Domain.Common;
using Domain.Entities;
using System.Linq.Expressions;
namespace Domain.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(Guid id);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task AddAsync(Cliente cliente);

    // Update parcial usando UpdateBuilder
    Task<int> UpdateAsync(Cliente cliente, Action<IUpdateBuilder<Cliente>>? configure = null);

    Task DeleteAsync(Guid id);
}