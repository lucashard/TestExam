using TestExam.Domain;

namespace TestExam;

public class MateriaPrimaService
{
    private readonly IMateriaPrimaRepository _repository;

    public MateriaPrimaService(IMateriaPrimaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MateriaPrima>> GetAllAsync(int? id = null, string? nombre = null, string? descripcion = null)
    {
        return await _repository.GetAllAsync(id, nombre, descripcion).ConfigureAwait(false);
    }

    public async Task<MateriaPrima?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<int> CreateAsync(string nombre, string descripcion, decimal cantidad)
    {
        ServiceGuard.Required(nombre, nameof(nombre));

        var materiaPrima = new MateriaPrima
        {
            Nombre = nombre,
            Descripcion = descripcion,
            Cantidad = cantidad
        };

        return await _repository.AddAsync(materiaPrima);
    }

    public async Task<bool> UpdateAsync(int id, string nombre, string descripcion, decimal cantidad)
    {
        var materiaPrima = new MateriaPrima
        {
            Id = id,
            Nombre = nombre,
            Descripcion = descripcion,
            Cantidad = cantidad
        };

        return await _repository.UpdateAsync(id, materiaPrima);
    }
}
