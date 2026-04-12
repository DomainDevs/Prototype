using System;

namespace Infrastructure.Common.Diagnostics;

public static class StartupDiagnostics
{
    public static void LogStartupError(Exception ex)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n" + new string('!', 65));
        Console.WriteLine(" !!! ERROR CRITICO DE CONFIGURACION !!!");
        Console.WriteLine(new string('!', 65));

        var rootCause = GetMostInnerException(ex);

        // 1. Detección de Inyección de Dependencias
        if (EsErrorInyeccion(ex))
        {
            TipificarErrorInyeccion(ex);
        }
        // 2. NUEVO: Detección de Base de Datos (Persistence)
        else if (EsErrorBaseDeDatos(ex, rootCause))
        {
            TipificarErrorBaseDeDatos(rootCause);
        }
        // 3. Detección de Dependencia Circular
        else if (ex.Message.Contains("A circular dependency was detected"))
        {
            Console.WriteLine(" [!] TIPO: DEPENDENCIA CIRCULAR");
            Console.WriteLine(" [i] DETALLE: Dos o mas clases se inyectan entre si infinitamente.");
        }
        else
        {
            Console.WriteLine(" [!] TIPO: ERROR GENERICO DE ARRANQUE");
            Console.WriteLine($" [>] MENSAJE: {ex.Message}");
        }

        // Mostrar causa raíz técnica
        if (rootCause != ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n [?] DETALLE TECNICO:");
            Console.WriteLine($" --> {rootCause.Message}");
        }

        Console.ResetColor();
        Console.WriteLine(new string('-', 65));

        ManejarPausaSegunEntorno();
    }

    private static void TipificarErrorBaseDeDatos(Exception root)
    {
        Console.WriteLine(" [!] TIPO: ERROR DE PERSISTENCIA / BASE DE DATOS");

        string sugerencia = "Verifica la cadena de conexión en 'appsettings.json'.";

        if (root.Message.Contains("network-related") || root.Message.Contains("server was not found"))
            sugerencia = "El servidor de Base de Datos no responde. ¿Está encendido?";
        else if (root.Message.Contains("login failed"))
            sugerencia = "Usuario o contraseña de la base de datos incorrectos.";
        else if (root.Message.Contains("relation") || root.Message.Contains("table") || root.Message.Contains("does not exist"))
            sugerencia = "Faltan tablas. ¿Olvidaste ejecutar 'dotnet ef database update'?";

        Console.WriteLine($" [x] ANALISIS: {sugerencia}");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n [+] PASOS DE VERIFICACION:");
        Console.WriteLine("  1. Revisa el ConnectionString en el archivo de configuración.");
        Console.WriteLine("  2. Asegurate de que el motor de BD (SQL Server/Postgres) esté corriendo.");
        Console.WriteLine("  3. Comprueba que la base de datos haya sido creada.");
    }

    private static void TipificarErrorInyeccion(Exception ex)
    {
        string fullMessage = GetMostInnerException(ex).Message;
        var parts = fullMessage.Split('\'');
        string missingType = parts.Length > 1 ? CleanTypeName(parts[1]) : "No identificado";
        string consumer = parts.Length > 3 ? CleanTypeName(parts[3]) : "Constructor";

        Console.WriteLine(" [!] TIPO: ERROR PROBABLE DE INYECCION DE DEPENDENCIAS (DI)");
        Console.WriteLine($" [x] PIEZA FALTANTE: {missingType}");
        Console.WriteLine($" [x] SOLICITADO POR: {consumer}");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n [+] PASOS DE VERIFICACION:");
        Console.WriteLine("  1. Comprueba el registro en 'DependencyInjection.cs'.");
        Console.WriteLine("  2. Verifica que el metodo .AddPersistence() o .AddInfrastructure() esté en Program.cs.");
    }

    // --- HELPERS ---

    private static bool EsErrorInyeccion(Exception ex) =>
        ex.Message.Contains("Unable to resolve service") ||
        (ex.InnerException?.Message.Contains("Unable to resolve service") ?? false);

    private static bool EsErrorBaseDeDatos(Exception ex, Exception root) =>
        ex.StackTrace?.Contains("Persistence") == true ||
        ex.StackTrace?.Contains("EntityFrameworkCore") == true ||
        root.Message.Contains("database", StringComparison.OrdinalIgnoreCase) ||
        root.Message.Contains("connection", StringComparison.OrdinalIgnoreCase);

    private static Exception GetMostInnerException(Exception ex)
    {
        while (ex.InnerException != null) ex = ex.InnerException;
        return ex;
    }

    private static string CleanTypeName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName)) return fullName;
        var index = fullName.LastIndexOf('.');
        return index != -1 ? fullName.Substring(index + 1) : fullName;
    }

    private static void ManejarPausaSegunEntorno()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (env == "Development")
        {
            Console.WriteLine("\n [MODO DESARROLLO] Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}