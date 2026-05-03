using System.Data.SqlClient;
using TestExam.Domain;

namespace TestExam.Repositories;

public class EtapasRepository(string connectionString) : SqlRepositoryBase(connectionString), IEtapasRepository
{
    public async Task<IEnumerable<Etapas>> GetAllAsync()
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, Nombre FROM Etapas", conn);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        var result = new List<Etapas>();
        while (await reader.ReadAsync())
        {
            result.Add(new Etapas { Id = reader.GetInt32(0), Nombre = reader.GetString(1) });
        }

        return result;
    }

    public async Task<Etapas?> GetByIdAsync(int id)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, Nombre FROM Etapas WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;
        return new Etapas { Id = reader.GetInt32(0), Nombre = reader.GetString(1) };
    }

    public async Task<int> AddAsync(Etapas etapa)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("INSERT INTO Etapas (Nombre) OUTPUT INSERTED.Id VALUES (@Nombre)", conn);
        cmd.Parameters.AddWithValue("@Nombre", etapa.Nombre);
        await conn.OpenAsync();
        var id = await cmd.ExecuteScalarAsync();
        return Convert.ToInt32(id);
    }

    public async Task<bool> UpdateAsync(int id, Etapas etapa)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("UPDATE Etapas SET Nombre = @Nombre WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@Nombre", etapa.Nombre);
        await conn.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }
}

