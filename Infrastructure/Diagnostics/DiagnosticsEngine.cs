using Infrastructure.Diagnostics;
using System.Reflection;

namespace Infrastructure.Diagnostics;

public static class DiagnosticsEngine
{
    public static DiagnosticsModel Build(Assembly assembly)
    {
        var repos = assembly.GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                t.Name.EndsWith("Register"))
            .Select(t => new DiagnosticsInfo(
                Name: t.Name,
                Namespace: t.Namespace ?? "unknown"
            ))
            .OrderBy(x => x.Name)
            .ToList();

        return new DiagnosticsModel(repos);
    }
}