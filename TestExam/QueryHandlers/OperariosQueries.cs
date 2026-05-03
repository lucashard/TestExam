using MediatR;
using TestExam.Domain;

namespace TestExam;

public record GetAllOperariosQuery() : IRequest<IEnumerable<Operarios>>;

public class GetAllOperariosHandler : IRequestHandler<GetAllOperariosQuery, IEnumerable<Operarios>>
{
    private readonly OperariosService _service;

    public GetAllOperariosHandler(OperariosService service)
    {
        _service = service;
    }

    public Task<IEnumerable<Operarios>> Handle(GetAllOperariosQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAllAsync();
    }
}

public record GetOperariosByIdQuery(int Id) : IRequest<Operarios?>;

public class GetOperariosByIdHandler : IRequestHandler<GetOperariosByIdQuery, Operarios?>
{
    private readonly OperariosService _service;

    public GetOperariosByIdHandler(OperariosService service)
    {
        _service = service;
    }

    public Task<Operarios?> Handle(GetOperariosByIdQuery request, CancellationToken cancellationToken)
    {
        return _service.GetByIdAsync(request.Id);
    }
}

