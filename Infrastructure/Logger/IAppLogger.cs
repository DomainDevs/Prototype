using System;

namespace Infrastructure.Logger;

public interface IAppLogger<T>
{
    void LogInfo(string message);
    void LogError(Exception ex, string message);
}
