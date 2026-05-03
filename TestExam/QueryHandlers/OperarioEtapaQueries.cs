using MediatR;
using TestExam.Domain;

namespace TestExam;

public record GetAllOperarioEtapaQuery() : IRequest<IEnumerable<OperarioEtapa>>;

public class GetAllOperarioEtapaHandler : IRequestHandler<GetAllOperarioEtapaQuery, IEnumerable<OperarioEtapa>>
{
    private readonly OperarioEtapaService _service;

    public GetAllOperarioEtapaHandler(OperarioEtapaService service)
    {
        _service = service;
    }

    public Task<IEnumerable<OperarioEtapa>> Handle(GetAllOperarioEtapaQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAllAsync();
    }
}

public record GetOperarioEtapaByIdQuery(int Id) : IRequest<OperarioEtapa?>;

public class GetOperarioEtapaByIdHandler : IRequestHandler<GetOperarioEtapaByIdQuery, OperarioEtapa?>
{
    private readonly OperarioEtapaService _service;

    public GetOperarioEtapaByIdHandler(OperarioEtapaService service)
    {
        _service = service;
    }

    public Task<OperarioEtapa?> Handle(GetOperarioEtapaByIdQuery request, CancellationToken cancellationToken)
    {
        return _service.GetByIdAsync(request.Id);
    }
}

