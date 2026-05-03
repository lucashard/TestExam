using MediatR;
using TestExam.Domain;

namespace TestExam;

public record GetAllDireccionesQuery() : IRequest<IEnumerable<Direccion>>;

public class GetAllDireccionesHandler : IRequestHandler<GetAllDireccionesQuery, IEnumerable<Direccion>>
{
    private readonly DireccionService _service;

    public GetAllDireccionesHandler(DireccionService service)
    {
        _service = service;
    }

    public Task<IEnumerable<Direccion>> Handle(GetAllDireccionesQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAllAsync();
    }
}

public record GetDireccionByIdQuery(int Id) : IRequest<Direccion?>;

public class GetDireccionByIdHandler : IRequestHandler<GetDireccionByIdQuery, Direccion?>
{
    private readonly DireccionService _service;

    public GetDireccionByIdHandler(DireccionService service)
    {
        _service = service;
    }

    public Task<Direccion?> Handle(GetDireccionByIdQuery request, CancellationToken cancellationToken)
    {
        return _service.GetByIdAsync(request.Id);
    }
}

