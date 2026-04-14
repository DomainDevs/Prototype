using System.Reflection;

namespace Infrastructure.Diagnostics;

public static class DiagnosticsEngine
{
    public static DiagnosticsModel Build(
        Assembly assembly,
        Func<Type, bool>? filter = null)
    {
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract);

        if (filter != null)
            types = types.Where(filter);

        var items = types
            .Select(t => new DiagnosticsInfo(
                t.Name,
                t.Namespace ?? "unknown"))
            .OrderBy(x => x.Name)
            .ToList();

        return new DiagnosticsModel(items);
    }

    public static Func<Type, bool> RegisterTypes()
        => t => t.Name.EndsWith("Register");

    public static Func<Type, bool> RepositoryTypes()
        => t => t.Name.EndsWith("Repository");

    public static Func<Type, bool> ServiceTypes()
        => t => t.Name.EndsWith("Service");
}