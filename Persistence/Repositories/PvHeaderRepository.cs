using DataToolkit.Library.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using System.Data;
using System.Linq.Expressions;

namespace Persistence.Repositories
{

    public class PvHeaderRepository : IPvHeaderRepository
    {
        private readonly GenericRepository<PvHeader> _repo;

        public PvHeaderRepository(IDbConnection connection)
        {
            _repo = new GenericRepository<PvHeader>(connection);
        }

        public Task<int> InsertAsync(PvHeader entity) => _repo.InsertAsync(entity);

        public Task<int> UpdateAsync(PvHeader entity, params Expression<Func<PvHeader, object>>[] includeProperties)
            => _repo.UpdateAsync(entity, includeProperties);

        public async Task<int> DeleteByIdAsync(int codSuc, int codRamo, long nroPol, int nroEndoso)
        {
            var entity = new PvHeader { CodSuc = codSuc, CodRamo = codRamo, NroPol = nroPol, NroEndoso = nroEndoso };
            return await _repo.DeleteAsync(entity);
        }

        public Task<PvHeader?> GetByIdAsync(int codSuc, int codRamo, long nroPol, int nroEndoso)
        {
            var entity = new PvHeader { CodSuc = codSuc, CodRamo = codRamo, NroPol = nroPol, NroEndoso = nroEndoso };
            return _repo.GetByIdAsync(entity);
        }

        public Task<IEnumerable<PvHeader>> GetAllAsync() => _repo.GetAllAsync();
    }
}