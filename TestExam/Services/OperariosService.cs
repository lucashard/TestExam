using TestExam.Domain;
using TestExam.Repositories;

namespace TestExam;

public class OperariosService
{
    private readonly IOperariosRepository _repository;

    public OperariosService(IOperariosRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Operarios>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Operarios?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<int> CreateAsync(string nombre)
    {
        ServiceGuard.Required(nombre, nameof(nombre));

        var operario = new Operarios
        {
            Nombre = nombre
        };

        return _repository.AddAsync(operario);
    }

    public Task<bool> UpdateAsync(int id, string nombre)
    {
        var operario = new Operarios
        {
            Id = id,
            Nombre = nombre
        };

        return _repository.UpdateAsync(id, operario);
    }
}

