// =========================================================
// Este mapper fue generado automáticamente por DataToolkit.
// Usa Riok.Mapperly para generar las implementaciones en build.
// =========================================================

using Application.Features.Cliente.DTOs;
using Application.Features.Cliente.Commands.Create;
using Application.Features.Cliente.Commands.Update;
using Riok.Mapperly.Abstractions;
using Entities = Domain.Entities;

namespace Application.Features.Cliente.Mappers;

[Mapper]
public static partial class ClienteMapper
{
    // DTO → Commands
    public static partial UpdateClienteCommand ToUpdateCommand(this ClienteRequestDto dto);
    public static partial CreateClienteCommand ToCommandCreate(this ClienteRequestDto dto);

    // Commands → Entity
    public static partial Entities.Cliente ToEntity(CreateClienteCommand command);
    public static partial Entities.Cliente ToEntity(UpdateClienteCommand command);

    // Entity → DTO
    public static partial ClienteResponseDto ToDto(Entities.Cliente entity);

}