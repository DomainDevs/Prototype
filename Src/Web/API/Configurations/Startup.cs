namespace API.Configurations
{

    using static System.Net.Mime.MediaTypeNames;

    internal static class Startup
    {
        internal static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((hostingContext, config) =>
            {
                const string Directory = "Configurations";
                IHostEnvironment env = hostingContext.HostingEnvironment;

                config.
                     AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                DirectoryInfo fi = new DirectoryInfo($"{Directory}/");
                if (fi.Exists)
                {
                    List<string> fileNames = fi.GetFiles("*.json").Select(f => f.Name).ToList();
                    foreach (string subDir in fileNames)
                    {
                        var filecom = subDir.Split('.');
                        config
                            .AddJsonFile($"{Directory}/{filecom[0]}.{filecom[1]}", optional: false, reloadOnChange: true)
                            .AddJsonFile($"{Directory}/{filecom[0]}.{env.EnvironmentName}.{filecom[1]}", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables();
                            
                    }
                    config.Build();
                }
            });
            return host;
        }
    }
}
