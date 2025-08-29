namespace TestExam;

public class MateriaPrimaService
{
    private readonly IMateriaPrimaRepository _repository;

    public MateriaPrimaService(IMateriaPrimaRepository repository)
    {
        _repository = repository;
    }

    public async Task<MateriaPrima?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<int> CreateAsync(string nombre)
    {
        var materiaPrima = new MateriaPrima { Nombre = nombre };
        return await _repository.AddAsync(materiaPrima);
    }
}
