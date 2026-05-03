using MediatR;

namespace TestExam;

public record CreateEtapasCommand(string Nombre) : IRequest<int>;

public class CreateEtapasHandler : IRequestHandler<CreateEtapasCommand, int>
{
    private readonly EtapasService _service;

    public CreateEtapasHandler(EtapasService service)
    {
        _service = service;
    }

    public Task<int> Handle(CreateEtapasCommand request, CancellationToken cancellationToken)
    {
        return _service.CreateAsync(request.Nombre);
    }
}

public record UpdateEtapasCommand(int Id, string Nombre) : IRequest<bool>;

public class UpdateEtapasHandler : IRequestHandler<UpdateEtapasCommand, bool>
{
    private readonly EtapasService _service;

    public UpdateEtapasHandler(EtapasService service)
    {
        _service = service;
    }

    public Task<bool> Handle(UpdateEtapasCommand request, CancellationToken cancellationToken)
    {
        return _service.UpdateAsync(request.Id, request.Nombre);
    }
}

