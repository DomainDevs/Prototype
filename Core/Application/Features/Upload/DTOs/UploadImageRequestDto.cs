using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Upload.DTOs;

public class UploadImageRequestDto
{
    /// <summary>Archivo recibido desde el frontend (ASP.NET)</summary>
    public IFormFileCollection Files { get; set; } = default!;

    /// <summary>Grupo de configuración (Images, PDF, etc.)</summary>
    public string GroupName { get; set; } = string.Empty;
}

