namespace Infrastructure.Diagnostics;

public static class DiagnosticsRenderer
{
    public static void Render(DiagnosticsModel model)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;

        var mode = string.IsNullOrWhiteSpace(model.Filter)
            ? "FULL"
            : $"FILTER: {model.Filter}";

        Console.WriteLine($"▶ DIAGNOSTICS ({mode})");

        foreach (var group in model.Groups)
        {
            if (!group.Value.Any()) continue;

            Console.WriteLine($"\n[{group.Key}]");

            foreach (var t in group.Value.OrderBy(x => x.Name))
                Console.WriteLine($"   - {t.Name}");
        }

        Console.ResetColor();
    }
}