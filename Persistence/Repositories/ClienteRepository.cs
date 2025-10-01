using DataToolkit.Library.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using System.Data;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly GenericRepository<Cliente> _repo;

        public ClienteRepository(IDbConnection connection)
        {
            _repo = new GenericRepository<Cliente>(connection);
        }

        public Task<int> InsertAsync(Cliente entity)
        {
            return _repo.InsertAsync(entity);
        }

        public Task<int> UpdateAsync(Cliente entity, params Expression<Func<Cliente, object>>[] includeProperties)
        {
            return _repo.UpdateAsync(entity, includeProperties);
        }

        public async Task<int> DeleteByIdAsync(int id)
        {
            var entity = new Cliente { Id = id };
            return await _repo.DeleteAsync(entity);
        }

        public Task<Cliente?> GetByIdAsync(int id)
        {
            var entity = new Cliente { Id = id };
            return _repo.GetByIdAsync(entity);
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            var Entity = await _repo.GetAllAsync();
            return Entity;
        }
    }
}