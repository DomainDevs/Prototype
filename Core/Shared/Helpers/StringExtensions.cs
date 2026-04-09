using System;
using System.Text;

namespace Shared.Helpers;

public static class GuidExtensions
{
    public static string ToShortString(this Guid guid, int length = 12)
    {
        return Convert.ToBase64String(guid.ToByteArray())
            .Replace("/", "_")
            .Replace("+", "-")
            .Replace("=", "")
            .Substring(0, length);
    }

    public static string GenerateShortId(int length = 12)
    {
        // Usamos un Guid para asegurar unicidad, lo pasamos a Base64 para acortarlo
        string base64Guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        // Limpiamos caracteres no seguros para nombres de archivos/URLs
        return base64Guid
            .Replace("/", "_")
            .Replace("+", "-")
            .Replace("=", "")
            .Substring(0, length);
    }
}
