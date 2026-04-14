using Infrastructure.Logger;
using Serilog;

public class SerilogAppLogger<T> : IAppLogger<T>
{
    private readonly ILogger _logger;

    public SerilogAppLogger(ILogger logger)
    {
        _logger = logger.ForContext<T>();
    }

    public void LogInfo(string message)
        => _logger.Information(message);

    public void LogError(Exception ex, string message)
        => _logger.Error(ex, message);
}