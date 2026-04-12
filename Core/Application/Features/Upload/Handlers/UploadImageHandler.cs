using Application.Features.Upload.Commands;
using MediatR;
using Shared.DTOs;
using Shared.Interfaces;

public class UploadFileHandler : IRequestHandler<UploadFileCommand, UploadFileResponse>
{
    private readonly IFileStorageService _fileStorageService;

    public UploadFileHandler(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public async Task<UploadFileResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        // Solo se pasa el groupName
        return await _fileStorageService.SaveFileAsync(
            request.GroupName,
            request.FileName,
            request.Content
        );
    }
}