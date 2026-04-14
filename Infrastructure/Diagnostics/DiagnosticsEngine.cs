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

        List<DiagnosticsInfo> Map(IEnumerable<Type> source) =>
            source.Where(Match)
                  .Select(t => new DiagnosticsInfo(
                      t.Name,
                      t.Namespace ?? "unknown"
                  ))
                  .ToList();

        return new DiagnosticsModel(
            new Dictionary<string, List<DiagnosticsInfo>>
            {
                ["SRVS"] = Map(types.Where(DiagnosticsClassifier.IsService)),
                ["UCASE"] = Map(types.Where(DiagnosticsClassifier.IsUseCase)),
                ["HNDL"] = Map(types.Where(DiagnosticsClassifier.IsHandler)),
                ["VAL"] = Map(types.Where(DiagnosticsClassifier.IsValidator))
            }
        );
    }
}