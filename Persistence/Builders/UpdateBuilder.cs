// Persistence/Builders/UpdateBuilder.cs
using Domain.Common;
using System.Linq.Expressions;

namespace Persistence.Builders;

public class UpdateBuilder<T> : IUpdateBuilder<T>
{
    private readonly Dictionary<string, object?> _changes = new();

    public void Set<TProperty>(Expression<Func<T, TProperty>> property, TProperty value)
    {
        if (property.Body is MemberExpression member)
        {
            _changes[member.Member.Name] = value;
        }
        else
        {
            throw new ArgumentException($"La expresión '{property}' no es una propiedad válida.");
        }
    }

    public IReadOnlyDictionary<string, object?> Changes => _changes;
}
