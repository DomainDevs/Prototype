using DataToolkit.Library.Repositories;
using Domain.Entities;
using DataToolkit.Library.UnitOfWorkLayer;
using System.Linq.Expressions;
using Interfaces = Domain.Interfaces;


namespace Persistence.Repositories
{
    public class ClienteRepository : Interfaces.IClienteRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private IGenericRepository<Cliente> Repo => _unitOfWork.GetRepository<Cliente>();

        public bool AutoCommit { get; set; } = true;

        public ClienteRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Cliente>> GetAllAsync() => Repo.GetAllAsync();

        public Task<Cliente?> GetByIdAsync(Cliente entity)
        {
            return Repo.GetByIdAsync(entity);
        }

        public async Task<int> InsertAsync(Cliente entity)
        {
            var result = await Repo.InsertAsync(entity);
            if (AutoCommit) _unitOfWork.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(Cliente entity, Expression<Func<Cliente, object>>? includeProperties = null)
        {
            var result = await Repo.UpdateAsync(entity, includeProperties);
            if (AutoCommit) _unitOfWork.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(Cliente entity)
        {
            var result = await Repo.DeleteAsync(entity);
            if (AutoCommit) _unitOfWork.Commit();
            return result;
        }
    }
}