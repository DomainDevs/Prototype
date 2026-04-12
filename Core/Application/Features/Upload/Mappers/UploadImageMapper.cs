using Application.Features.Upload.Commands;
using Application.Features.Upload.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Upload.Mappers;

public static class UploadImageMapper
{
    public static UploadImageCommand From(IFormFile file, string groupName, Stream stream)
    {
        if (file == null)
            throw new ArgumentNullException(nameof(file));

        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        if (string.IsNullOrWhiteSpace(groupName))
            throw new ArgumentException("GroupName no puede ser vacío", nameof(groupName));

        return new UploadImageCommand
        {
            GroupName = groupName,
            FileName = file.FileName,
            ContentType = file.ContentType,
            Content = stream,
            Size = file.Length
        };
    }
}
