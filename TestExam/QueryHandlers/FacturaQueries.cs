using MediatR;
using TestExam.Domain;

namespace TestExam;

public record GetAllFacturaQuery() : IRequest<IEnumerable<Factura>>;

public class GetAllFacturaHandler : IRequestHandler<GetAllFacturaQuery, IEnumerable<Factura>>
{
    private readonly FacturaService _service;

    public GetAllFacturaHandler(FacturaService service)
    {
        _service = service;
    }

    public Task<IEnumerable<Factura>> Handle(GetAllFacturaQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAllAsync();
    }
}

public record GetFacturaByIdQuery(int Id) : IRequest<Factura?>;

public class GetFacturaByIdHandler : IRequestHandler<GetFacturaByIdQuery, Factura?>
{
    private readonly FacturaService _service;

    public GetFacturaByIdHandler(FacturaService service)
    {
        _service = service;
    }

    public Task<Factura?> Handle(GetFacturaByIdQuery request, CancellationToken cancellationToken)
    {
        return _service.GetByIdAsync(request.Id);
    }
}

