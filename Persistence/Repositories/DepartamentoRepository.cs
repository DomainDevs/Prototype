using DataToolkit.Library.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using System.Data;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly GenericRepository<Departamento> _repo;

        public DepartamentoRepository(IDbConnection connection)
        {
            _repo = new GenericRepository<Departamento>(connection);
        }

        public Task<int> InsertAsync(Departamento entity)
        {
            return _repo.InsertAsync(entity);
        }

        public Task<int> UpdateAsync(Departamento entity, params Expression<Func<Departamento, object>>[] includeProperties)
        {
            return _repo.UpdateAsync(entity, includeProperties);
        }

        public Task<IEnumerable<Departamento>> GetAllAsync(params Expression<Func<Departamento, object>>[]? selectProperties)
        {
            return _repo.GetAllAsync(selectProperties);
        }


        public Task<Departamento?> GetByIdAsync(int id, params Expression<Func<Departamento, object>>[]? selectProperties)
        {
            var entity = new Departamento { Id = id };
            return _repo.GetByIdAsync(entity, selectProperties);
        }

        public Task<int> DeleteByIdAsync(int id)
        {
            var entity = new Departamento { Id = id };
            return _repo.DeleteAsync(entity);
        }
    }
}

