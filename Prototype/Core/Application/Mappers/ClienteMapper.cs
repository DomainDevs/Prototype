// =========================================================
// Este mapper fue generado automáticamente por DataToolkit.
// Usa Riok.Mapperly para generar las implementaciones en build.
// =========================================================

using Application.DTOs;
using Application.Features.Cliente.Commands.Create;
using Application.Features.Cliente.Commands.Update;
using Application.Features.Cliente.Commands.Delete;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
public static partial class ClienteMapper
{
    // DTO → Commands
    public static partial UpdateClienteCommand ToUpdateCommand(this ClienteRequestDto dto);
    public static partial CreateClienteCommand ToCommandCreate(this ClienteRequestDto dto);

    // Commands → Entity
    public static partial Cliente ToEntity(CreateClienteCommand command);
    public static partial Cliente ToEntity(UpdateClienteCommand command);

    // Entity → DTO
    public static partial ClienteRequestDto ToDto(Cliente entity);
}