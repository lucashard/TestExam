using MediatR;

namespace TestExam;

public record CreateMateriaPrimaCommand(string Nombre) : IRequest<int>;

public class CreateMateriaPrimaHandler : IRequestHandler<CreateMateriaPrimaCommand, int>
{
    private readonly MateriaPrimaService _service;

    public CreateMateriaPrimaHandler(MateriaPrimaService service)
    {
        _service = service;
    }

    public async Task<int> Handle(CreateMateriaPrimaCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request.Nombre);
    }
}
