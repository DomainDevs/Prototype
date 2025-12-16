using DataToolkit.Library.Common;     // MultiMapRequest
using DataToolkit.Library.Sql;        // ISqlExecutor
using Application.Interfaces;

namespace Persistence.Fluent;

public class QueryEngine : IQueryEngine
{
    private readonly ISqlExecutor _executor;

    public QueryEngine(ISqlExecutor executor)
    {
        _executor = executor;
    }

    // ------------------------------------------------------
    // 1) Consultas simples
    // ------------------------------------------------------
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        return await _executor.FromSqlAsync<T>(sql, parameters);
    }

    // ------------------------------------------------------
    // 2) Consultas JOIN (multi-mapping)
    // ------------------------------------------------------
    public async Task<IEnumerable<T>> QueryMultiMapAsync<T>(object request)
    {
        if (request is not MultiMapRequest mmRequest)
            throw new ArgumentException("Invalid MultiMapRequest object.", nameof(request));

        return await _executor.FromSqlMultiMapAsync<T>(mmRequest);
    }

    // ------------------------------------------------------
    // 3) Múltiples SELECTs (QueryMultiple)
    // ------------------------------------------------------
    public async Task<List<IEnumerable<dynamic>>> QueryMultipleAsync(string sql, object? parameters = null)
    {
        return await _executor.QueryMultipleAsync(sql, parameters);
    }
}
