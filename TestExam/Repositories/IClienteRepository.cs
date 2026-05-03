using TestExam.Domain;

namespace TestExam.Repositories;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    Task<int> AddAsync(Cliente cliente);
    Task<bool> UpdateAsync(int id, Cliente cliente);
}

