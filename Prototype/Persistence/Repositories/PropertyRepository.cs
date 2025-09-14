using Domain.Entities;
using DataToolkit.Library.Sql;
using Interfaces = Domain.Interfaces;

namespace Persistence.Repositories;

public class PropertyRepository : Interfaces.IPropertyRepository
{
    private readonly ISqlExecutor _executor;

    public PropertyRepository(ISqlExecutor executor)
    {
        _executor = executor;
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        var sql = "SELECT Id, Title, Address, Price FROM Properties";
        return await _executor.FromSqlAsync<Property>(sql);
    }

    public async Task<Property?> GetByIdAsync(int id)
    {
        var sql = "SELECT Id, Title, Address, Price FROM Properties WHERE Id = @Id";
        var result = await _executor.FromSqlAsync<Property>(sql, new { Id = id });
        return result.FirstOrDefault();
    }
}
