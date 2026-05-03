using MediatR;

namespace TestExam;

public record CreateOperarioEtapaCommand(int EtapaId, int OperarioId, decimal Duracion) : IRequest<int>;

public class CreateOperarioEtapaHandler : IRequestHandler<CreateOperarioEtapaCommand, int>
{
    private readonly OperarioEtapaService _service;

    public CreateOperarioEtapaHandler(OperarioEtapaService service)
    {
        _service = service;
    }

    public Task<int> Handle(CreateOperarioEtapaCommand request, CancellationToken cancellationToken)
    {
        return _service.CreateAsync(request.EtapaId, request.OperarioId, request.Duracion);
    }
}

public record UpdateOperarioEtapaCommand(int Id, int EtapaId, int OperarioId, decimal Duracion) : IRequest<bool>;

public class UpdateOperarioEtapaHandler : IRequestHandler<UpdateOperarioEtapaCommand, bool>
{
    private readonly OperarioEtapaService _service;

    public UpdateOperarioEtapaHandler(OperarioEtapaService service)
    {
        _service = service;
    }

    public Task<bool> Handle(UpdateOperarioEtapaCommand request, CancellationToken cancellationToken)
    {
        return _service.UpdateAsync(request.Id, request.EtapaId, request.OperarioId, request.Duracion);
    }
}

