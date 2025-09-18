using TestExam.Domain;
using System.Threading.Tasks;

namespace TestExam.Infrastructure;

public class MateriaPrimaRepository : IMateriaPrimaRepository
{
    private static readonly List<MateriaPrima> _materias = new()
    {
        new MateriaPrima { Id = 1, Nombre = "Harina", Descripcion = "Harina de trigo" },
        new MateriaPrima { Id = 2, Nombre = "Azúcar", Descripcion = "Azúcar refinada" }
    };

    public Task<MateriaPrima?> GetByIdAsync(int id)
    {
        return Task.FromResult(_materias.FirstOrDefault(m => m.Id == id));
    }

    public Task<int> AddAsync(MateriaPrima materiaPrima)
    {
        materiaPrima.Id = _materias.Count + 1;
        _materias.Add(materiaPrima);
        return Task.FromResult(materiaPrima.Id);
    }
}
