// Este record es solo un contenedor de datos para MediatR
using Application.DTOs;
using MediatR;

public record UpdateClienteCommand(int Id, string Nombre, string Apellido, string? Email) : IRequest<int>;