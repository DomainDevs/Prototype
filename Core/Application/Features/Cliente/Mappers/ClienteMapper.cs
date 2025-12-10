// =========================================================
// Este mapper fue generado automáticamente por DataToolkit.
// Usa Riok.Mapperly para generar las implementaciones en build.
// =========================================================

using Application.Features.Cliente.DTOs;
using Application.Features.Cliente.Commands;
using Riok.Mapperly.Abstractions;
using Entities = Domain.Entities;

namespace Application.Features.Cliente.Mappers;

[Mapper(AllowNullPropertyAssignment = true, RequiredMappingStrategy = RequiredMappingStrategy.None)]
public static partial class ClienteMapper
{
    // DTO → Commands
    public static partial ClienteUpdateCommand ToUpdateCommand(this ClienteUpdateRequestDto dto);
    public static partial ClienteCreateCommand ToCommandCreate(this ClienteCreateRequestDto dto);

    // Commands → Entity
    public static partial Entities.Cliente ToEntity(ClienteCreateCommand command);
    public static partial Entities.Cliente ToEntity(ClienteUpdateCommand command);

    // Entity → DTO
    public static partial ClienteQueryResponseDto ToDto(Entities.Cliente entity);

}