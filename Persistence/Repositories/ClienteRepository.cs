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

        public Task<IEnumerable<Cliente>> GetAllAsync(params Expression<Func<Cliente, object>>[]? selectProperties)
        {
            return _repo.GetAllAsync(selectProperties);
        }


        public Task<Cliente?> GetByIdAsync(int id, params Expression<Func<Cliente, object>>[]? selectProperties)
        {
            var entity = new Cliente { Id = id };
            return _repo.GetByIdAsync(entity, selectProperties);
        }

        public Task<int> DeleteByIdAsync(int id)
        {
            var entity = new Cliente { Id = id };
            return _repo.DeleteAsync(entity);
        }
    }
}
