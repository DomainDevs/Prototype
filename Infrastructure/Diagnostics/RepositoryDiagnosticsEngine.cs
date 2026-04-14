using Infrastructure.Diagnostics;
using System.Reflection;

internal static class RepositoryDiagnosticsEngine
{
    public static RepositoryDiagnosticsModel Build(Assembly assembly)
    {
        var repos = assembly.GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                t.Name.EndsWith("Repository"))
            .Select(t => new RepositoryInfo(
                Name: t.Name,
                Namespace: t.Namespace ?? "unknown"
            ))
            .OrderBy(x => x.Name)
            .ToList();

        return new RepositoryDiagnosticsModel(repos);
    }
}