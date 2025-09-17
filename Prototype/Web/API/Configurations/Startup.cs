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
                IHostEnvironment env = hostingContext.HostingEnvironment;
                DirectoryInfo directory = new DirectoryInfo(ConfigDirectory);

                if (directory.Exists)
                {
                    foreach (FileInfo file in directory.EnumerateFiles("*.json"))
                    {
                        string baseName = Path.GetFileNameWithoutExtension(file.Name);
                        string extension = Path.GetExtension(file.Name);

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
