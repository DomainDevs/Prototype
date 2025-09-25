// =========================================================
// Este mapper fue generado automáticamente por DataToolkit.
// Usa Riok.Mapperly para generar las implementaciones en build.
// =========================================================

using Application.Features.Poliza.DTOs;
using Application.Features.Poliza.Commands.Create;
using Application.Features.Poliza.Commands.Update;
using Riok.Mapperly.Abstractions;
using Entities = Domain.Entities;

namespace Application.Features.Poliza.Mappers;

[Mapper]
public static partial class PvHeaderMapper
{
    // DTO → Commands
    public static partial UpdatePvHeaderCommand ToUpdateCommand(this PvHeaderRequestDto dto);
    public static partial CreatePvHeaderCommand ToCommandCreate(this PvHeaderRequestDto dto);

    // Commands → Entity
    public static partial Entities.PvHeader ToEntity(CreatePvHeaderCommand command);
    public static partial Entities.PvHeader ToEntity(UpdatePvHeaderCommand command);

    // Entity → DTO
    public static partial PvHeaderResponseDto ToDto(Entities.PvHeader entity);

}