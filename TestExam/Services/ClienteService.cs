using TestExam.Domain;
using TestExam.Repositories;

namespace TestExam;

public class ClienteService
{
    private readonly IClienteRepository _repository;

    public ClienteService(IClienteRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<Cliente?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<int> CreateAsync(string nombre, string apellido, string documento, string email, string telefono, int direccionId)
    {
        ServiceGuard.Required(nombre, nameof(nombre));
        ServiceGuard.Required(apellido, nameof(apellido));
        ServiceGuard.Required(documento, nameof(documento));
        ServiceGuard.Required(email, nameof(email));
        ServiceGuard.Required(telefono, nameof(telefono));
        ServiceGuard.Positive(direccionId, nameof(direccionId));

        var cliente = new Cliente
        {
            Nombre = nombre,
            Apellido = apellido,
            Documento = documento,
            Email = email,
            Telefono = telefono,
            DireccionId = direccionId
        };

        return _repository.AddAsync(cliente);
    }

    public Task<bool> UpdateAsync(int id, string nombre, string apellido, string documento, string email, string telefono, int direccionId)
    {
        var cliente = new Cliente
        {
            Id = id,
            Nombre = nombre,
            Apellido = apellido,
            Documento = documento,
            Email = email,
            Telefono = telefono,
            DireccionId = direccionId
        };

        return _repository.UpdateAsync(id, cliente);
    }
}

