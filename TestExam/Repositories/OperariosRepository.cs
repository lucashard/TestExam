using System.Data.SqlClient;
using TestExam.Domain;

namespace TestExam.Repositories;

public class OperariosRepository(string connectionString) : SqlRepositoryBase(connectionString), IOperariosRepository
{
    public async Task<IEnumerable<Operarios>> GetAllAsync()
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, Nombre FROM Operarios", conn);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        var result = new List<Operarios>();
        while (await reader.ReadAsync())
        {
            result.Add(new Operarios { Id = reader.GetInt32(0), Nombre = reader.GetString(1) });
        }

        return result;
    }

    public async Task<Operarios?> GetByIdAsync(int id)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, Nombre FROM Operarios WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;
        return new Operarios { Id = reader.GetInt32(0), Nombre = reader.GetString(1) };
    }

    public async Task<int> AddAsync(Operarios operario)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("INSERT INTO Operarios (Nombre) OUTPUT INSERTED.Id VALUES (@Nombre)", conn);
        cmd.Parameters.AddWithValue("@Nombre", operario.Nombre);
        await conn.OpenAsync();
        var id = await cmd.ExecuteScalarAsync();
        return Convert.ToInt32(id);
    }

    public async Task<bool> UpdateAsync(int id, Operarios operario)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("UPDATE Operarios SET Nombre = @Nombre WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@Nombre", operario.Nombre);
        await conn.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }
}

