
namespace Infrastructure.Configuration;

public class UploadOptions
{
    public Dictionary<string, UploadGroupOptions> UploadGroups { get; set; } = new();
}
