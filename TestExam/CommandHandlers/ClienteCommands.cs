using MediatR;

namespace TestExam;

public record CreateClienteCommand(
    string Nombre,
    string Apellido,
    string Documento,
    string Email,
    string Telefono,
    int DireccionId) : IRequest<int>;

public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, int>
{
    private readonly ClienteService _service;

    public CreateClienteHandler(ClienteService service)
    {
        _service = service;
    }

    public Task<int> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        return _service.CreateAsync(request.Nombre, request.Apellido, request.Documento, request.Email, request.Telefono, request.DireccionId);
    }
}

public record UpdateClienteCommand(
    int Id,
    string Nombre,
    string Apellido,
    string Documento,
    string Email,
    string Telefono,
    int DireccionId) : IRequest<bool>;

public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand, bool>
{
    private readonly ClienteService _service;

    public UpdateClienteHandler(ClienteService service)
    {
        _service = service;
    }

    public Task<bool> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        return _service.UpdateAsync(request.Id, request.Nombre, request.Apellido, request.Documento, request.Email, request.Telefono, request.DireccionId);
    }
}

