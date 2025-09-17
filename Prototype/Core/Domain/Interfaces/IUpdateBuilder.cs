// Domain/Common/IUpdateBuilder.cs
using System.Linq.Expressions;

namespace Domain.Common;

public interface IUpdateBuilder<T>
{
    // Marca una propiedad para actualizar con un nuevo valor
    void Set<TProperty>(Expression<Func<T, TProperty>> property, TProperty value);
}