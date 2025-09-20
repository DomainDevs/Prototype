// =========================================================
// Este mapper fue generado automáticamente por DataToolkit.
// Usa Riok.Mapperly para generar las implementaciones en build.
// =========================================================

using Application.DTOs;
using Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

public static class ClienteCommandMapper
{
    public static UpdateClienteCommand ToCommand(this ClienteRequestDto dto)
    {
        return new UpdateClienteCommand(
            dto.Id,
            dto.Nombre,
            dto.Apellido,
            dto.Email
        );
    }
}
