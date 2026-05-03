using TestExam.Domain;

namespace TestExam.Repositories;

public interface IEtapasRepository
{
    Task<IEnumerable<Etapas>> GetAllAsync();
    Task<Etapas?> GetByIdAsync(int id);
    Task<int> AddAsync(Etapas etapa);
    Task<bool> UpdateAsync(int id, Etapas etapa);
}

