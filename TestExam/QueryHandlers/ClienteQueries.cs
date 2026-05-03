using MediatR;
using TestExam.Domain;

namespace TestExam;

public record GetAllClientesQuery() : IRequest<IEnumerable<Cliente>>;

public class GetAllClientesHandler : IRequestHandler<GetAllClientesQuery, IEnumerable<Cliente>>
{
    private readonly ClienteService _service;

    public GetAllClientesHandler(ClienteService service)
    {
        _service = service;
    }

    public Task<IEnumerable<Cliente>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAllAsync();
    }
}

public record GetClienteByIdQuery(int Id) : IRequest<Cliente?>;

public class GetClienteByIdHandler : IRequestHandler<GetClienteByIdQuery, Cliente?>
{
    private readonly ClienteService _service;

    public GetClienteByIdHandler(ClienteService service)
    {
        _service = service;
    }

    public Task<Cliente?> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        return _service.GetByIdAsync(request.Id);
    }
}

