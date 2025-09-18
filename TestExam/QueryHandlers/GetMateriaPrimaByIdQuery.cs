using MediatR;
using TestExam.Domain;

namespace TestExam;

// Query: Puerto de entrada para obtener una MateriaPrima por ID
public record GetMateriaPrimaByIdQuery(int Id) : IRequest<MateriaPrima?>;

// Handler: Adaptador de entrada que usa el servicio de aplicación
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
