using MediatR;
using TestExam.Domain;

namespace TestExam;

public record GetAllEtapasQuery() : IRequest<IEnumerable<Etapas>>;

public class GetAllEtapasHandler : IRequestHandler<GetAllEtapasQuery, IEnumerable<Etapas>>
{
    private readonly EtapasService _service;

    public GetAllEtapasHandler(EtapasService service)
    {
        _service = service;
    }

    public Task<IEnumerable<Etapas>> Handle(GetAllEtapasQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAllAsync();
    }
}

public record GetEtapasByIdQuery(int Id) : IRequest<Etapas?>;

public class GetEtapasByIdHandler : IRequestHandler<GetEtapasByIdQuery, Etapas?>
{
    private readonly EtapasService _service;

    public GetEtapasByIdHandler(EtapasService service)
    {
        _service = service;
    }

    public Task<Etapas?> Handle(GetEtapasByIdQuery request, CancellationToken cancellationToken)
    {
        return _service.GetByIdAsync(request.Id);
    }
}

