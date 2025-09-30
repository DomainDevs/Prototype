using API.Configurations;
using Application;
using Infrastructure;
using Persistence;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.AddConfigurations();                           //Cargar archivos de configuración json

    builder.Services.AddControllers();                          //Cargar controllers
    builder.Services.AddInfrastructure(builder.Configuration);  //Inyectar capa infraestructura
    builder.Services.AddPersistence(builder.Configuration);     //Inyectar capa persistencia
    builder.Services.AddApplication();                          //inyectar capa de aplicación

    var app = builder.Build();

    app.UseInfrastructure(builder.Configuration); //Habilitar uso de Middlewares

    // Configure the HTTP request pipeline.
    /*
    if (app.Environment.IsDevelopment()) { 
        app.MapOpenApi(); // Swagger solo en dev
    }
    */

    app.MapControllers();
    app.Run();
    
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    Console.WriteLine("Error iniciando el servicio", ex.Message);
}
finally
{ 
}
