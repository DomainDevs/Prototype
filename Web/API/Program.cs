using API.Configurations;
using Application;
using Infrastructure;
using Persistence;
using Serilog;
using Infrastructure.Common.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;

try
{
    BootConsole.WriteBanner("[NAME]");

    var builder = WebApplication.CreateBuilder(args);
    bool isDev = builder.Environment.IsDevelopment();
    bool enableVerboseLogs = isDev ||
        builder.Configuration.GetValue<bool>("Diagnostics:EnableVerbose");

    BootConsole.Step("1/5", "Configurando Host y Serilog...");
    builder.Host.AddConfigurations().UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

    BootConsole.Step("2/5", "Inyectando dependencias y escaneando assemblies (Scrutor)...");
    builder.Services.AddControllers();
    builder.Services
        .AddInfrastructure(builder.Configuration, isDev)
        .AddPersistence(builder.Configuration, isDev, enableVerboseLogs)
        .AddApplication(isDev, enableVerboseLogs);

    BootConsole.Step("3/5", "Construyendo ServiceProvider y contenedor...");
    var app = builder.Build();

    BootConsole.Step("4/5", "Configurando Pipeline HTTP y middleware de seguridad...");
    app.UseInfrastructure(builder.Configuration, isDev);
    app.MapControllers();

    BootConsole.Step("5/5", "¡Servicio desplegado con éxito!");

    Log.Information("Servicio iniciando correctamente...");
    app.Run();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    StartupDiagnostics.LogStartupError(ex);
}
finally
{
    Log.Information(">> Servicio finalizado y recursos liberados <<");
    Log.CloseAndFlush();
}

internal static class BootConsole
{
    public static void WriteBanner(string name)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n » INICIANDO SERVICIO {name}...");
        Console.ResetColor();
    }

    public static void Step(string step, string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($" Ok [{step}] ");
        Console.ResetColor();
        Console.WriteLine(msg);
    }
}