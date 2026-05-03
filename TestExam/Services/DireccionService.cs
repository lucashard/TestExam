using TestExam.Domain;
using TestExam.Repositories;

namespace TestExam;

public class DireccionService
{
    private readonly IDireccionRepository _repository;

    public DireccionService(IDireccionRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Direccion>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Direccion?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<int> CreateAsync(string nombre, int altura)
    {
        ServiceGuard.Required(nombre, nameof(nombre));
        ServiceGuard.Positive(altura, nameof(altura));

        var direccion = new Direccion
        {
            Nombre = nombre,
            Altura = altura
        };

        return _repository.AddAsync(direccion);
    }

    public Task<bool> UpdateAsync(int id, string nombre, int altura)
    {
        var direccion = new Direccion
        {
            Id = id,
            Nombre = nombre,
            Altura = altura
        };

        return _repository.UpdateAsync(id, direccion);
    }
}

