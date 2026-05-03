using MediatR;

namespace TestExam;

public record UpdateMateriaPrimaCommand(int Id, string Nombre, string Descripcion = "", decimal Cantidad = 0) : IRequest<bool>;

public class UpdateMateriaPrimaHandler : IRequestHandler<UpdateMateriaPrimaCommand, bool>
{
    private readonly MateriaPrimaService _service;

    public UpdateMateriaPrimaHandler(MateriaPrimaService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(UpdateMateriaPrimaCommand request, CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(request.Id, request.Nombre, request.Descripcion, request.Cantidad);
    }
}

