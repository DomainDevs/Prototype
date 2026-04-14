namespace Infrastructure.Diagnostics;

internal static class RepositoryDiagnosticsRenderer
{
    public static void Render(RepositoryDiagnosticsModel model)
    {
        if (model.Repositories.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    ▶ [DI] No repositories detected.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("    ▶ [DI] Repository Diagnostics:");

        foreach (var repo in model.Repositories)
        {
            Console.WriteLine($"       {repo.Name}  ({repo.Namespace})");
        }

        Console.ResetColor();
    }
}