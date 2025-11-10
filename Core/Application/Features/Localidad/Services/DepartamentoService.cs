// DepartamentoService.cs
using Domain.Interfaces;
using Application.Features.Localidad.DTOs;
using Application.Features.Localidad.Mappers;

namespace Application.Features.Localidad.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _repo;

        public DepartamentoService(IDepartamentoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<DepartamentoQueryResponseDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(e => DepartamentoMapper.ToDto(e));
        }

        public async Task<DepartamentoQueryResponseDto?> GetByIdAsync(int Id)
        {
            var entity = await _repo.GetByIdAsync(Id);
            if (entity == null) return null;

            return DepartamentoMapper.ToDto(entity);
        }

        public async Task<int> CreateAsync(DepartamentoCreateRequestDto dto)
        {
            var entity = DepartamentoMapper.ToEntity(dto);
            return await _repo.InsertAsync(entity);
        }

        public async Task UpdateAsync(DepartamentoUpdateRequestDto dto)
        {
            var entity = DepartamentoMapper.ToEntity(dto);
            await _repo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var affected = await _repo.DeleteByIdAsync(Id);
            return affected > 0;
        }

    }
}