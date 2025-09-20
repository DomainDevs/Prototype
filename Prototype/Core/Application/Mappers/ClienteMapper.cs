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
    public static partial UpdateClienteCommand ToCommand(ClienteRequestDto dto);
}