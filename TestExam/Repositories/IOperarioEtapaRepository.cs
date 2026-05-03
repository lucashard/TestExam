using TestExam.Domain;

namespace TestExam.Repositories;

public interface IOperarioEtapaRepository
{
    Task<IEnumerable<OperarioEtapa>> GetAllAsync();
    Task<OperarioEtapa?> GetByIdAsync(int id);
    Task<int> AddAsync(OperarioEtapa operarioEtapa);
    Task<bool> UpdateAsync(int id, OperarioEtapa operarioEtapa);
}

