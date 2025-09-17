using Domain.Common; // ✅ nuevo
using DataToolkit.Library.Repositories;
using DataToolkit.Library.UnitOfWorkLayer;
using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IUnitOfWork _uow;

        public ClienteRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<IEnumerable<Cliente>> GetAllAsync()
        {
            var repo = _uow.GetRepository<Cliente>();
            return repo.GetAllAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            var repo = _uow.GetRepository<Cliente>();
            return await repo.GetByIdAsync(new Cliente { Id = id });
        }

        public async Task<int> InsertAsync(Cliente cliente)
        {
            var repo = _uow.GetRepository<Cliente>();
            var result = await repo.InsertAsync(cliente);
            _uow.Commit();
            return result;
        }

        public async Task<int> UpdateAsync(
            Cliente cliente,
            Action<IUpdateBuilder<Cliente>>? configure = null
        )
        {
            var repo = _uow.GetRepository<Cliente>();
            var result = await repo.UpdateAsync(cliente, configure);
            _uow.Commit();
            return result;
        }

        public async Task<int> DeleteAsync(Cliente cliente)
        {
            var repo = _uow.GetRepository<Cliente>();
            var result = await repo.DeleteAsync(cliente);
            _uow.Commit();
            return result;
        }
    }
}
