using TestExam.Domain;

namespace TestExam.Repositories;

public interface IDireccionRepository
{
    Task<IEnumerable<Direccion>> GetAllAsync();
    Task<Direccion?> GetByIdAsync(int id);
    Task<int> AddAsync(Direccion direccion);
    Task<bool> UpdateAsync(int id, Direccion direccion);
}

