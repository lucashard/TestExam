using MediatR;

namespace TestExam;

public record CreateFacturaCommand(int ClienteId, IEnumerable<int>? EtapasIds = null) : IRequest<int>;

public class CreateFacturaHandler : IRequestHandler<CreateFacturaCommand, int>
{
    private readonly FacturaService _service;

    public CreateFacturaHandler(FacturaService service)
    {
        _service = service;
    }

    public Task<int> Handle(CreateFacturaCommand request, CancellationToken cancellationToken)
    {
        return _service.CreateAsync(request.ClienteId, request.EtapasIds);
    }
}

public record UpdateFacturaCommand(int Id, int ClienteId, IEnumerable<int>? EtapasIds = null) : IRequest<bool>;

public class UpdateFacturaHandler : IRequestHandler<UpdateFacturaCommand, bool>
{
    private readonly FacturaService _service;

    public UpdateFacturaHandler(FacturaService service)
    {
        _service = service;
    }

    public Task<bool> Handle(UpdateFacturaCommand request, CancellationToken cancellationToken)
    {
        return _service.UpdateAsync(request.Id, request.ClienteId, request.EtapasIds);
    }
}

