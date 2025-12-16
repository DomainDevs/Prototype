using DataToolkit.Library.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Fluent;

internal sealed class QueryCommand : IQueryCommand
{
    private readonly ISqlExecutor _executor;
    private readonly string _sql;

    private object? _params;
    private int? _timeout;

    public QueryCommand(ISqlExecutor executor, string sql)
    {
        _executor = executor;
        _sql = sql;
    }

    public IQueryCommand Params(object parameters)
    {
        _params = parameters;
        return this;
    }

    public IQueryCommand Timeout(int seconds)
    {
        _timeout = seconds;
        return this;
    }

    public Task<IEnumerable<T>> RunAsync<T>()
    {
        return _executor.FromSqlAsync<T>(_sql, _params, _timeout);
    }
}
