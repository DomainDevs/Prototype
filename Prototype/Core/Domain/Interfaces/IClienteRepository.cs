using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(int id);
    Task<int> InsertAsync(Cliente cliente);
}