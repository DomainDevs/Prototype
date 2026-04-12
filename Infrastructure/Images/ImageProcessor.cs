using SkiaSharp;

namespace Infrastructure.Images;

public class ImageProcessor : IImageProcessor
{
    private const long MaxByteSize = 4 * 1024 * 1024; // 4MB
    private const int MinWidth = 500;
    private const int MinHeight = 400;

    public async Task<byte[]> ConvertToWebpAsync(
        Stream inputStream,
        int maxWidth = 1200,
        int maxHeight = 800,
        int quality = 75,
        string? watermarkText = "Hogar360")
    {
        // 1. Validación de tamaño rápida
        if (inputStream.CanSeek && inputStream.Length > MaxByteSize)
            throw new InvalidOperationException("La imagen excede el límite de 4MB.");

        using var memoryStream = new MemoryStream();
        await inputStream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        using var codec = SKCodec.Create(memoryStream);
        if (codec == null) throw new Exception("Formato de imagen no soportado.");

        // 2. Manejo de Orientación EXIF (Clave para fotos de celular)
        var origin = codec.EncodedOrigin;
        using var originalBitmap = SKBitmap.Decode(codec);
        if (originalBitmap == null) throw new Exception("No se pudo decodificar.");

        // Aplicamos la rotación si el metadato EXIF lo indica
        using var orientedBitmap = HandleOrientation(originalBitmap, origin);

        // 3. Validación de dimensiones mínimas
        if (orientedBitmap.Width < MinWidth || orientedBitmap.Height < MinHeight)
            throw new InvalidOperationException($"Imagen demasiado pequeña ({orientedBitmap.Width}x{orientedBitmap.Height}).");

        // 4. Cálculo de dimensiones finales
        double ratio = Math.Min((double)maxWidth / orientedBitmap.Width, (double)maxHeight / orientedBitmap.Height);
        int finalWidth = ratio < 1 ? (int)(orientedBitmap.Width * ratio) : orientedBitmap.Width;
        int finalHeight = ratio < 1 ? (int)(orientedBitmap.Height * ratio) : orientedBitmap.Height;

        // 5. Renderizado y Marca de Agua
        using var workingBitmap = new SKBitmap(finalWidth, finalHeight);
        using (var canvas = new SKCanvas(workingBitmap))
        {
            canvas.Clear(SKColors.Transparent);

            using var paint = new SKPaint
            {
                IsAntialias = true,
                FilterQuality = SKFilterQuality.High
            };

            // Dibujamos con el resize integrado
            canvas.DrawBitmap(orientedBitmap, new SKRect(0, 0, finalWidth, finalHeight), paint);

            ApplyWatermark(canvas, finalWidth, finalHeight, watermarkText);
        }

        // 6. Codificación WebP
        using var image = SKImage.FromBitmap(workingBitmap);
        using var data = image.Encode(SKEncodedImageFormat.Webp, quality);

        return data.ToArray();
    }

    private SKBitmap HandleOrientation(SKBitmap bitmap, SKEncodedOrigin origin)
    {
        // Si la orientación es normal, retornamos el original (sin crear copias extras)
        if (origin == SKEncodedOrigin.TopLeft) return bitmap;

        SKBitmap rotated;
        switch (origin)
        {
            case SKEncodedOrigin.RightTop: // Rotar 90 CW
                rotated = new SKBitmap(bitmap.Height, bitmap.Width);
                using (var canvas = new SKCanvas(rotated))
                {
                    canvas.Translate(bitmap.Height, 0);
                    canvas.RotateDegrees(90);
                    canvas.DrawBitmap(bitmap, 0, 0);
                }
                break;
            case SKEncodedOrigin.BottomRight: // Rotar 180
                rotated = new SKBitmap(bitmap.Width, bitmap.Height);
                using (var canvas = new SKCanvas(rotated))
                {
                    canvas.RotateDegrees(180, bitmap.Width / 2f, bitmap.Height / 2f);
                    canvas.DrawBitmap(bitmap, 0, 0);
                }
                break;
            case SKEncodedOrigin.LeftBottom: // Rotar 270 CW
                rotated = new SKBitmap(bitmap.Height, bitmap.Width);
                using (var canvas = new SKCanvas(rotated))
                {
                    canvas.Translate(0, bitmap.Width);
                    canvas.RotateDegrees(270);
                    canvas.DrawBitmap(bitmap, 0, 0);
                }
                break;
            default: return bitmap;
        }
        return rotated;
    }

    private void ApplyWatermark(SKCanvas canvas, int width, int height, string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return;

        float dynamicTextSize = width * 0.04f;
        if (dynamicTextSize < 16) dynamicTextSize = 16;

        using var paint = new SKPaint
        {
            Color = SKColors.White.WithAlpha(170),
            TextSize = dynamicTextSize,
            IsAntialias = true,
            // Usamos un fallback seguro por si Arial no está en el servidor
            Typeface = SKTypeface.FromFamilyName("sans-serif", SKFontStyle.Bold),
            Style = SKPaintStyle.Fill
        };

        // Sombra para contraste en cualquier fondo de pared/piso
        paint.ImageFilter = SKImageFilter.CreateDropShadow(1, 1, 2, 2, SKColors.Black.WithAlpha(120));

        float margin = width * 0.03f;
        float x = width - paint.MeasureText(text) - margin;
        float y = height - margin;

        canvas.DrawText(text, x, y, paint);
    }
}