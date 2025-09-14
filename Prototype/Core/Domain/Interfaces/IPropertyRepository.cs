using Domain.Entities;

namespace Domain.Interfaces;

//Tus contratos de repositorio de Domain/Application
//La implementacion del repositorio PropertyRepository => Capa: Persistence.Repositories;
public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<Property?> GetByIdAsync(int id);
}