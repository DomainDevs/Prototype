using Microsoft.AspNetCore.Mvc;
using Prototype.Infrastructure;
using API.Configurations;
using Serilog;
using Prototype.Application;
using API.Conventions;


StaticLogger.EnsureInitialized();
Log.Information("Server Booting Up...");
try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.AddConfigurations();
    builder.Host.UseSerilog((_, config) =>
    {
        config.WriteTo.Console()
        .ReadFrom.Configuration(builder.Configuration);
    });
    
    
    builder.Services.AddControllers(options =>
    {
        options.Conventions.Add(new GroupingByNamespaceConvention());
    });

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    var app = builder.Build();

    app.UseInfrastructure(builder.Configuration);

    app.MapControllers();

    app.Run();


}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    StaticLogger.EnsureInitialized();
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}
