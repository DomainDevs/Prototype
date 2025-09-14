namespace API.Configurations
{

    using static System.Net.Mime.MediaTypeNames;

    internal static class Startup
    {
        private const string ConfigDirectory = "Configurations";

        internal static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                var directory = new DirectoryInfo(ConfigDirectory);

                if (directory.Exists)
                {
                    foreach (var file in directory.EnumerateFiles("*.json"))
                    {
                        var baseName = Path.GetFileNameWithoutExtension(file.Name);
                        var extension = Path.GetExtension(file.Name);

                        // archivo base
                        config.AddJsonFile(
                            Path.Combine(ConfigDirectory, file.Name),
                            optional: false,
                            reloadOnChange: true);

                        // archivo específico por entorno
                        config.AddJsonFile(
                            Path.Combine(ConfigDirectory, $"{baseName}.{env.EnvironmentName}{extension}"),
                            optional: true,
                            reloadOnChange: true);
                    }
                }

                // siempre agregar variables de entorno
                config.AddEnvironmentVariables();
            });

            return host;
        }
    }
}
