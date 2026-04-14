namespace Infrastructure.Diagnostics;

public static class DiagnosticsRenderer
{
    public static void Render(DiagnosticsModel model)
    {
        foreach (var group in model.Groups)
        {
            Console.WriteLine($"[{group.Key}]");

            foreach (var item in group.Value)
                Console.WriteLine($"   {item.Name} ({item.Namespace})");
        }
    }
}