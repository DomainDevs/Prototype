
using DataToolkit.Library.UnitOfWorkLayer;
using Domain.Entities;

namespace Persistence.Repositories
{
    public class ClienteRepository : Domain.Interfaces.IClienteRepository
    {
        private readonly IUnitOfWork _uow;

        public ClienteRepository(IUnitOfWork uow)
        {
            _uow = uow;
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

            _uow.Commit(); // ✅ tu implementación es sincrónica
            return result;
        }
    }
}
