using SkiaSharp;

namespace Infrastructure.Images;

public class WatermarkService : IWatermarkService
{
    private static readonly SKTypeface Typeface = SKTypeface.Default;

    public SKBitmap Apply(SKBitmap bitmap, string text)
    {
        // ⚡ NO COPIAR bitmap (usar directo reduce memoria y CPU)
        using var canvas = new SKCanvas(bitmap);

        float fontSize = Math.Max(24, bitmap.Width / 40);

        using var paint = new SKPaint
        {
            Color = SKColors.White.WithAlpha(180),
            TextSize = fontSize,
            IsAntialias = true,
            Typeface = Typeface,
            HintingLevel = SKPaintHinting.Normal
        };

        float x = 20;
        float y = bitmap.Height - 30;

        // ⚡ sombra sin crear otro SKPaint (más rápido)
        paint.Color = SKColors.Black.WithAlpha(120);
        canvas.DrawText(text, x + 2, y + 2, paint);

        // texto principal
        paint.Color = SKColors.White.WithAlpha(180);
        canvas.DrawText(text, x, y, paint);

        return bitmap;
    }
}