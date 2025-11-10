// IDepartamentoService.cs
using Application.Features.Localidad.DTOs;

namespace Application.Features.Localidad.Services;

public interface IDepartamentoService
{
    Task<IEnumerable<DepartamentoQueryResponseDto>> GetAllAsync();
    Task<DepartamentoQueryResponseDto?> GetByIdAsync(int Id);
    Task<int> CreateAsync(DepartamentoCreateRequestDto dto);
    Task UpdateAsync(DepartamentoUpdateRequestDto dto);
    Task<bool> DeleteAsync(int Id);
}