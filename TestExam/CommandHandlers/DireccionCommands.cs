using MediatR;

namespace TestExam;

public record CreateDireccionCommand(string Nombre, int Altura) : IRequest<int>;

public class CreateDireccionHandler : IRequestHandler<CreateDireccionCommand, int>
{
    private readonly DireccionService _service;

    public CreateDireccionHandler(DireccionService service)
    {
        _service = service;
    }

    public Task<int> Handle(CreateDireccionCommand request, CancellationToken cancellationToken)
    {
        return _service.CreateAsync(request.Nombre, request.Altura);
    }
}

public record UpdateDireccionCommand(int Id, string Nombre, int Altura) : IRequest<bool>;

public class UpdateDireccionHandler : IRequestHandler<UpdateDireccionCommand, bool>
{
    private readonly DireccionService _service;

    public UpdateDireccionHandler(DireccionService service)
    {
        _service = service;
    }

    public Task<bool> Handle(UpdateDireccionCommand request, CancellationToken cancellationToken)
    {
        return _service.UpdateAsync(request.Id, request.Nombre, request.Altura);
    }
}

