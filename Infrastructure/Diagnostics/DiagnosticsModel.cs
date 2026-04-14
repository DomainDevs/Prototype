namespace Infrastructure.Diagnostics;

public sealed record DiagnosticsModel(
    Dictionary<string, List<DiagnosticsInfo>> Groups
);