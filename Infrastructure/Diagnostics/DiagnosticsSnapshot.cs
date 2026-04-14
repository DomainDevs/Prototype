using System.Reflection;

namespace Infrastructure.Diagnostics;

public static class DiagnosticsSnapshot
{
    public static List<Type> Capture(params Assembly[] assemblies)
    {
        return assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract)
            .ToList();
    }
}