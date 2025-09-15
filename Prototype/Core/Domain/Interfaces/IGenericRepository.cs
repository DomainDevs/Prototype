namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<int> InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
