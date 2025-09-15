using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    Task<int> InsertAsync(Cliente cliente);
    Task<int> UpdateAsync(Cliente cliente);
    Task<int> DeleteAsync(Cliente cliente);
}