using MediatR;

namespace TestExam;

public record GetMateriaPrimaByIdQuery(int Id) : IRequest<MateriaPrima?>;

public class GetMateriaPrimaByIdHandler : IRequestHandler<GetMateriaPrimaByIdQuery, MateriaPrima?>
{

    private readonly MateriaPrimaService _service;

    public GetMateriaPrimaByIdHandler(MateriaPrimaService service)
    {
        _service = service;
    }

    public async Task<MateriaPrima?> Handle(GetMateriaPrimaByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}
