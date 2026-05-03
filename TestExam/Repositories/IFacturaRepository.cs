using TestExam.Domain;

namespace TestExam.Repositories;

public interface IFacturaRepository
{
    Task<IEnumerable<Factura>> GetAllAsync();
    Task<Factura?> GetByIdAsync(int id);
    Task<int> AddAsync(Factura factura);
    Task<bool> UpdateAsync(int id, Factura factura);
}

