namespace Infrastructure.Images;

public interface IImageProcessor
{
    /// <summary>
    /// Convierte y redimensiona una imagen a formato WebP.
    /// </summary>
    /// <param name="inputStream">Stream de la imagen original.</param>
    /// <param name="maxWidth">Ancho máximo permitido (Default 1200 para web).</param>
    /// <param name="maxHeight">Alto máximo permitido (Default 800 para web).</param>
    /// <param name="quality">Calidad de compresión (1-100).</param>
    /// <returns>Array de bytes de la imagen procesada.</returns>
    Task<byte[]> ConvertToWebpAsync(
        Stream inputStream, 
        int maxWidth = 1200, 
        int maxHeight = 800, 
        int quality = 75, 
        string? watermarkText = "Hogar360");
}