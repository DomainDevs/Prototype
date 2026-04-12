using Application.Features.Upload.Commands;
using Application.Features.Upload.DTOs;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Upload.Mappers
{
    public static class UploadFileMapper
    {
        public static UploadFileCommand ToCommand(this IFormFile file, string groupName, Stream content)
        {
            return new UploadFileCommand
            {
                GroupName = groupName,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Content = content,
                Size = file.Length
            };
        }
    }
}
