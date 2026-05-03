using TestExam.Domain;
using TestExam.Repositories;

namespace TestExam;

public class OperarioEtapaService
{
    private readonly IOperarioEtapaRepository _repository;

    public OperarioEtapaService(IOperarioEtapaRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<OperarioEtapa>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<OperarioEtapa?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<int> CreateAsync(int etapaId, int operarioId, decimal duracion)
    {
        ServiceGuard.Positive(etapaId, nameof(etapaId));
        ServiceGuard.Positive(operarioId, nameof(operarioId));
        ServiceGuard.Positive(duracion, nameof(duracion));

        var operarioEtapa = new OperarioEtapa
        {
            EtapaId = etapaId,
            OperarioId = operarioId,
            Duracion = duracion
        };

        return _repository.AddAsync(operarioEtapa);
    }

    public Task<bool> UpdateAsync(int id, int etapaId, int operarioId, decimal duracion)
    {
        var operarioEtapa = new OperarioEtapa
        {
            Id = id,
            EtapaId = etapaId,
            OperarioId = operarioId,
            Duracion = duracion
        };

        return _repository.UpdateAsync(id, operarioEtapa);
    }
}

