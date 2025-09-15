using Domain.Interfaces;
using DTRepo = DataToolkit.Library.Repositories;
using DataToolkit.Library.Metadata;

namespace Persistence.Repositories;

//GENERIC REPOSITORY - Con un Adapter
//❌ Si expusieras directamente el repo de DataToolkit a Application, ahí sí romperías la independencia.
//✅ Al envolverlo con un Adapter en Persistence, es correcto y no estás atando Application a la librería.
public class GenericRepositoryAdapter<T> : IGenericRepository<T> where T : class, new()
{
    private readonly DTRepo.IGenericRepository<T> _inner;

    public GenericRepositoryAdapter(DTRepo.IGenericRepository<T> inner)
    {
        _inner = inner;
    }

    public Task<int> InsertAsync(T entity) => _inner.InsertAsync(entity);

    public Task<int> DeleteAsync(T entity) => _inner.DeleteAsync(entity);

    public Task<IEnumerable<T>> GetAllAsync() => _inner.GetAllAsync();

    public async Task<T?> GetByIdAsync(object id)
    {
        var meta = EntityMetadataHelper.GetMetadata<T>();
        var keyProp = meta.KeyProperties.FirstOrDefault();

        if (keyProp == null)
            throw new InvalidOperationException(
                $"La entidad {typeof(T).Name} no tiene clave primaria definida en metadata.");

        var entity = new T();
        keyProp.SetValue(entity, id);

        return await _inner.GetByIdAsync(entity);
    }

    public Task<int> UpdateAsync(T entity) => _inner.UpdateAsync(entity);

    public Task<IEnumerable<T>> ExecuteStoredProcedureAsync(string storedProcedure, object parameters) =>
        _inner.ExecuteStoredProcedureAsync(storedProcedure, parameters);

    public Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcedure, object parameters) =>
        _inner.ExecuteStoredProcedureAsync<TResult>(storedProcedure, parameters);
}
