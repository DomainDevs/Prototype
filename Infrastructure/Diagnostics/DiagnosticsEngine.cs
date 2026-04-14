namespace Infrastructure.Diagnostics;

public static class DiagnosticsEngine
{
    public static DiagnosticsModel Build(List<Type> types, string? filter = null)
    {
        bool hasFilter = !string.IsNullOrWhiteSpace(filter);

        bool Match(Type t) =>
            !hasFilter ||
            t.Name.Contains(filter!, StringComparison.OrdinalIgnoreCase) ||
            (t.Namespace?.Contains(filter!, StringComparison.OrdinalIgnoreCase) ?? false);

        return new DiagnosticsModel
        {
            Filter = filter,
            Groups = new Dictionary<string, List<Type>>
            {
                ["Services"] = types.Where(t => DiagnosticsClassifier.IsService(t) && Match(t)).ToList(),
                ["UseCases"] = types.Where(t => DiagnosticsClassifier.IsUseCase(t) && Match(t)).ToList(),
                ["Handlers"] = types.Where(t => DiagnosticsClassifier.IsHandler(t) && Match(t)).ToList(),
                ["Validators"] = types.Where(t => DiagnosticsClassifier.IsValidator(t) && Match(t)).ToList()
            }
        };
    }
}