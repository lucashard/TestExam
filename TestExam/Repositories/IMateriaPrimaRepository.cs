namespace TestExam;

public interface IMateriaPrimaRepository
{
    Task<int> AddAsync(MateriaPrima materiaPrima);
    Task<MateriaPrima?> GetByIdAsync(int id);
}
