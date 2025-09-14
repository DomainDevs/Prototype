using Application.DTOs;
using Domain.Interfaces;
using Shared;

namespace Application.Features.Properties.Queries;

public class GetPropertyQueryHandler
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertyQueryHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<Result<IEnumerable<PropertyDto>>> Handle(GetAllPropertiesQuery query)
    {
        var properties = await _propertyRepository.GetAllAsync();
        var propertyDtos = properties.Select(p => new PropertyDto(p.Id, p.Title, p.Address, p.Price));
        return Result<IEnumerable<PropertyDto>>.Success(propertyDtos);
    }

    public async Task<Result<PropertyDto>> Handle(GetPropertyQuery query)
    {
        var property = await _propertyRepository.GetByIdAsync(query.Id);
        if (property == null)
        {
            return Result<PropertyDto>.Failure($"Propiedad con ID {query.Id} no encontrada.");
        }
        var propertyDto = new PropertyDto(property.Id, property.Title, property.Address, property.Price);
        return Result<PropertyDto>.Success(propertyDto);
    }
}
