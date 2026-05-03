namespace TestExam;

public interface IMateriaPrimaRepository
{
    Task<int> AddAsync(MateriaPrima materiaPrima);
    Task<MateriaPrima?> GetByIdAsync(int id);
    Task<IEnumerable<MateriaPrima>> GetAllAsync(int? id = null, string? nombre = null, string? descripcion = null);
    Task<bool> UpdateAsync(int id, MateriaPrima materiaPrima);
}
