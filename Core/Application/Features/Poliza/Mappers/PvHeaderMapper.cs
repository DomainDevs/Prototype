// =========================================================
// Este mapper fue generado automáticamente por DataToolkit.
// Usa Riok.Mapperly para generar las implementaciones en build.
// =========================================================

using Application.Features.Poliza.DTOs;
using Application.Features.Poliza.Commands;
using Riok.Mapperly.Abstractions;
using Entities = Domain.Entities;

namespace Application.Features.Poliza.Mappers;

[Mapper(AllowNullPropertyAssignment = true, RequiredMappingStrategy = RequiredMappingStrategy.None)]
public static partial class PvHeaderMapper
{
    // DTO → Commands
    public static partial PvHeaderUpdateCommand ToUpdateCommand(this PvHeaderUpdateRequestDto dto);
    public static partial PvHeaderCreateCommand ToCommandCreate(this PvHeaderCreateRequestDto dto);

    // Commands → Entity
    public static partial Entities.PvHeader ToEntity(PvHeaderCreateCommand command);
    public static partial Entities.PvHeader ToEntity(PvHeaderUpdateCommand command);

    // Entity → DTO
    public static partial PvHeaderQueryResponseDto ToDto(Entities.PvHeader entity);

}


