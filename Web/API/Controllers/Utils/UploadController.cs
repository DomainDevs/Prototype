using Application.Features.Upload.DTOs;
using Application.Features.Upload.Commands;
using Application.Features.Upload.Mappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace API.Controllers.Utils;

[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IMediator _mediator;

    public UploadController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [RequestSizeLimit(9_000_000)]
    public async Task<IActionResult> UploadFiles([FromForm] UploadImageRequestDto dto)
    {
        if (dto.Files == null || dto.Files.Count == 0)
            return BadRequest("No se enviaron archivos.");

        if (string.IsNullOrWhiteSpace(dto.GroupName))
            return BadRequest("Debe especificarse el grupo de carga.");

        var uploadResults = new List<UploadFileResponse>();

        foreach (var file in dto.Files)
        {
            if (file.Length == 0)
                continue;

            try
            {
                await using var stream = file.OpenReadStream();

                var command = UploadImageMapper.From(file, dto.GroupName, stream);

                var result = await _mediator.Send(command);

                uploadResults.Add(result);
            }
            catch (Exception ex)
            {
                uploadResults.Add(new UploadFileResponse
                {
                    FileName = file.FileName,
                    Url = string.Empty,
                    ContentType = $"Error: {ex.Message}",
                    Size = 0
                });
            }
        }

        return Ok(uploadResults);
    }
}