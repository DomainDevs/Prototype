namespace Infrastructure.Diagnostics;

internal static class DiagnosticsRenderer
{
    public static void Render(DiagnosticsModel model)
    {
        if (model.Register.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    ▶ [DI] No Register detected.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("    ▶ [DI] Register Diagnostics:");

        foreach (var repo in model.Register)
        {
            Console.WriteLine($"       {repo.Name}  ({repo.Namespace})");
        }

        Console.ResetColor();
    }
}