namespace Application.Interfaces;

public interface IQueryEngine
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);

    // Para consultas JOIN complejas
    Task<IEnumerable<T>> QueryMultiMapAsync<T>(object request);

    // Para múltiples SELECT
    Task<List<IEnumerable<dynamic>>> QueryMultipleAsync(string sql, object? parameters = null);
}
