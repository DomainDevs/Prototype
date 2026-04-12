using SkiaSharp;

namespace Infrastructure.Images;

public interface IWatermarkService
{
    SKBitmap Apply(SKBitmap bitmap, string text);
}