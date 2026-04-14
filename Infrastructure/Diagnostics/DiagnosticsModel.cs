namespace Infrastructure.Diagnostics;

public class DiagnosticsModel
{
    public string? Filter { get; set; }
    public Dictionary<string, List<Type>> Groups { get; set; } = new();
}