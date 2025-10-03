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

        public Task<int> InsertAsync(PvHeader entity)
        {
            return _repo.InsertAsync(entity);
        }

        public Task<int> UpdateAsync(PvHeader entity, params Expression<Func<PvHeader, object>>[] includeProperties)
        {
            return _repo.UpdateAsync(entity, includeProperties);
        }

        public Task<IEnumerable<PvHeader>> GetAllAsync(params Expression<Func<PvHeader, object>>[]? selectProperties)
        {
            return _repo.GetAllAsync(selectProperties);
        }


        public Task<PvHeader?> GetByIdAsync(int codSuc, int codRamo, long nroPol, int nroEndoso, params Expression<Func<PvHeader, object>>[]? selectProperties)
        {
            var entity = new PvHeader { CodSuc = codSuc, CodRamo = codRamo, NroPol = nroPol, NroEndoso = nroEndoso };
            return _repo.GetByIdAsync(entity, selectProperties);
        }

        public Task<int> DeleteByIdAsync(int codSuc, int codRamo, long nroPol, int nroEndoso)
        {
            var entity = new PvHeader { CodSuc = codSuc, CodRamo = codRamo, NroPol = nroPol, NroEndoso = nroEndoso };
            return _repo.DeleteAsync(entity);
        }
    }
}
