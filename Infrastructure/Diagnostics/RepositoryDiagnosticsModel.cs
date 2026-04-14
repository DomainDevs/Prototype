namespace Infrastructure.Diagnostics;

internal sealed record RepositoryDiagnosticsModel(
    List<RepositoryInfo> Repositories
);