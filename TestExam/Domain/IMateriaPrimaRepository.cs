using System.Threading.Tasks;

namespace TestExam.Domain;

public interface IMateriaPrimaRepository
{
    Task<MateriaPrima?> GetByIdAsync(int id);
    Task<int> AddAsync(MateriaPrima materiaPrima);
}
