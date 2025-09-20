// =========================================================
// Este mapper fue generado automáticamente por DataToolkit.
// Usa Riok.Mapperly para generar las implementaciones en build.
// =========================================================

using Application.DTOs;
using Application.Features.Clientes.Commands;
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

    //entity  → Dto
    public static partial ClienteRequestDto ToDto(Cliente entity);
}
