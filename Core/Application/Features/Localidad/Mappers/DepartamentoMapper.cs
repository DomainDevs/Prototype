// =========================================================
// Este mapper fue generado automáticamente por DataToolkit.
// Usa Riok.Mapperly para generar las implementaciones en build.
// =========================================================

using Application.Features.Localidad.DTOs;
using Riok.Mapperly.Abstractions;
using Entities = Domain.Entities;

namespace Application.Features.Localidad.Mappers;

[Mapper(AllowNullPropertyAssignment = true, RequiredMappingStrategy = RequiredMappingStrategy.None)]
public static partial class DepartamentoMapper
{
    // DTO -> Entity
    public static partial Entities.Departamento ToEntity(DepartamentoCreateRequestDto dto);
    public static partial Entities.Departamento ToEntity(DepartamentoUpdateRequestDto dto);

    // Entity -> DTO
    public static partial DepartamentoQueryResponseDto ToDto(Entities.Departamento entity);

}