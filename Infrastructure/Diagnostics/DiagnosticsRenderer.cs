namespace Infrastructure.Diagnostics;

public static class DiagnosticsRenderer
{
    public static void Render(DiagnosticsModel model)
    {
        if (model.Items.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    ▶ [DI] No items detected.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("    ▶ [DI] Diagnostics snapshot:");

        foreach (var item in model.Items)
        {
            Console.WriteLine($"       {item.Name} ({item.Namespace})");
        }

        Console.ResetColor();
    }
}