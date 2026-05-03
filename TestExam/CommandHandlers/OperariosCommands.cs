using MediatR;

namespace TestExam;

public record CreateOperariosCommand(string Nombre) : IRequest<int>;

public class CreateOperariosHandler : IRequestHandler<CreateOperariosCommand, int>
{
    private readonly OperariosService _service;

    public CreateOperariosHandler(OperariosService service)
    {
        _service = service;
    }

    public Task<int> Handle(CreateOperariosCommand request, CancellationToken cancellationToken)
    {
        return _service.CreateAsync(request.Nombre);
    }
}

public record UpdateOperariosCommand(int Id, string Nombre) : IRequest<bool>;

public class UpdateOperariosHandler : IRequestHandler<UpdateOperariosCommand, bool>
{
    private readonly OperariosService _service;

    public UpdateOperariosHandler(OperariosService service)
    {
        _service = service;
    }

    public Task<bool> Handle(UpdateOperariosCommand request, CancellationToken cancellationToken)
    {
        return _service.UpdateAsync(request.Id, request.Nombre);
    }
}

