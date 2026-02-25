
namespace Infrastructure.Configuration;

public class UploadGroupOptions
{
    public string FolderPath { get; set; } = string.Empty;
    public string[] AllowedExtensions { get; set; } = Array.Empty<string>();
    public long MaxFileSizeBytes { get; set; }
    public int MaxWidth { get; set; }

}