using TestExam.Domain;
using TestExam.Repositories;

namespace TestExam;

public class FacturaService
{
    private readonly IFacturaRepository _repository;

    public FacturaService(IFacturaRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Factura>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Factura?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<int> CreateAsync(int clienteId, IEnumerable<int>? etapasIds)
    {
        ServiceGuard.Positive(clienteId, nameof(clienteId));

        var factura = new Factura
        {
            ClienteId = clienteId,
            Etapas = MapEtapas(etapasIds)
        };

        return _repository.AddAsync(factura);
    }

    public Task<bool> UpdateAsync(int id, int clienteId, IEnumerable<int>? etapasIds)
    {
        var factura = new Factura
        {
            Id = id,
            ClienteId = clienteId,
            Etapas = MapEtapas(etapasIds)
        };

        return _repository.UpdateAsync(id, factura);
    }

    private static List<Etapas> MapEtapas(IEnumerable<int>? etapasIds)
    {
        if (etapasIds is null)
        {
            return [];
        }

        return etapasIds
            .Distinct()
            .Where(id => id > 0)
            .Select(id => new Etapas { Id = id })
            .ToList();
    }
}

