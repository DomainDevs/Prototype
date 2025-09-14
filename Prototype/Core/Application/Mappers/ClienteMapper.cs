// =========================================================
// Este mapper fue generado automáticamente por DataToolkit.
// Usa Riok.Mapperly para generar las implementaciones en build.
// =========================================================

using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
public static partial class ClienteMapper
{
    /// <summary>
    /// Convierte un ClienteRequestDto (DTO) en Cliente (Entidad de Dominio).
    /// </summary>
    public static partial Cliente ToEntity(ClienteRequestDto dto);

    /// <summary>
    /// Convierte un Cliente (Entidad de Dominio) en ClienteRequestDto (DTO).
    /// </summary>
    public static partial ClienteRequestDto ToDto(Cliente entity);
}