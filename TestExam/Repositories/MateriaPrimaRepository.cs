using System.Data.SqlClient;

namespace TestExam;

public class MateriaPrimaRepository : IMateriaPrimaRepository
{
    private readonly string _connectionString;

    public MateriaPrimaRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<int> AddAsync(MateriaPrima materiaPrima)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand("INSERT INTO MateriaPrima (Nombre) OUTPUT INSERTED.Id VALUES (@Nombre)", conn);
        cmd.Parameters.AddWithValue("@Nombre", materiaPrima.Nombre);
        await conn.OpenAsync();
        return (int)await cmd.ExecuteScalarAsync();
    }

    public async Task<MateriaPrima?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand("SELECT Id, Nombre FROM MateriaPrima WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new MateriaPrima
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1)
            };
        }
        return null;
    }
}
