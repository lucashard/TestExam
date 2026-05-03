using TestExam.Domain;
using System.Threading.Tasks;

namespace TestExam.Infrastructure;

public class MateriaPrimaRepository : IMateriaPrimaRepository
{
    private static readonly List<MateriaPrima> _materias = new()
    {
        new MateriaPrima { Id = 1, Nombre = "Harina", Descripcion = "Harina de trigo", Cantidad = 100 },
        new MateriaPrima { Id = 2, Nombre = "Azucar", Descripcion = "Azucar refinada", Cantidad = 200 }
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

    public Task<IEnumerable<MateriaPrima>> GetAllAsync(int? id = null, string? nombre = null, string? descripcion = null)
    {
        IEnumerable<MateriaPrima> result = _materias;
        if (id.HasValue) result = result.Where(m => m.Id == id.Value);
        if (!string.IsNullOrWhiteSpace(nombre)) result = result.Where(m => m.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrWhiteSpace(descripcion)) result = result.Where(m => m.Descripcion.Contains(descripcion, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(result);
    }

    public Task<bool> UpdateAsync(int id, MateriaPrima materiaPrima)
    {
        var current = _materias.FirstOrDefault(m => m.Id == id);
        if (current is null)
        {
            return Task.FromResult(false);
        }

        current.Nombre = materiaPrima.Nombre;
        current.Descripcion = materiaPrima.Descripcion;
        current.Cantidad = materiaPrima.Cantidad;
        return Task.FromResult(true);
    }
}
