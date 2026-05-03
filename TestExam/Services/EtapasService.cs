using TestExam.Domain;
using TestExam.Repositories;

namespace TestExam;

public class EtapasService
{
    private readonly IEtapasRepository _repository;

    public EtapasService(IEtapasRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Etapas>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Etapas?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<int> CreateAsync(string nombre)
    {
        ServiceGuard.Required(nombre, nameof(nombre));

        var etapa = new Etapas
        {
            Nombre = nombre
        };

        return _repository.AddAsync(etapa);
    }

    public Task<bool> UpdateAsync(int id, string nombre)
    {
        var etapa = new Etapas
        {
            Id = id,
            Nombre = nombre
        };

        return _repository.UpdateAsync(id, etapa);
    }
}

