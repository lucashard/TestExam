using TestExam.Domain;

namespace TestExam.Repositories;

public interface IOperariosRepository
{
    Task<IEnumerable<Operarios>> GetAllAsync();
    Task<Operarios?> GetByIdAsync(int id);
    Task<int> AddAsync(Operarios operario);
    Task<bool> UpdateAsync(int id, Operarios operario);
}

