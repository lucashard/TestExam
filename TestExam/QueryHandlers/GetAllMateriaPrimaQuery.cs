using MediatR;

namespace TestExam;

public record GetAllMateriaPrimaQuery(int? Id = null, string? Nombre = null, string? Descripcion = null)
    : IRequest<IEnumerable<MateriaPrima>>;

public class GetAllMateriaPrimaHandler : IRequestHandler<GetAllMateriaPrimaQuery, IEnumerable<MateriaPrima>>
{
    private readonly MateriaPrimaService _service;

    public GetAllMateriaPrimaHandler(MateriaPrimaService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<MateriaPrima>> Handle(GetAllMateriaPrimaQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync(request.Id, request.Nombre, request.Descripcion);
    }
}

