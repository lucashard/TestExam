using System.Data.SqlClient;
using TestExam.Domain;

namespace TestExam.Repositories;

public class OperarioEtapaRepository(string connectionString) : SqlRepositoryBase(connectionString), IOperarioEtapaRepository
{
    public async Task<IEnumerable<OperarioEtapa>> GetAllAsync()
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, EtapaId, OperarioId, Duracion FROM OperarioEtapa", conn);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        var result = new List<OperarioEtapa>();
        while (await reader.ReadAsync())
        {
            result.Add(Map(reader));
        }

        return result;
    }

    public async Task<OperarioEtapa?> GetByIdAsync(int id)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, EtapaId, OperarioId, Duracion FROM OperarioEtapa WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;
        return Map(reader);
    }

    public async Task<int> AddAsync(OperarioEtapa operarioEtapa)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand(@"INSERT INTO OperarioEtapa (EtapaId, OperarioId, Duracion)
                                         OUTPUT INSERTED.Id
                                         VALUES (@EtapaId, @OperarioId, @Duracion)", conn);
        cmd.Parameters.AddWithValue("@EtapaId", operarioEtapa.EtapaId);
        cmd.Parameters.AddWithValue("@OperarioId", operarioEtapa.OperarioId);
        cmd.Parameters.AddWithValue("@Duracion", operarioEtapa.Duracion);
        await conn.OpenAsync();
        var id = await cmd.ExecuteScalarAsync();
        return Convert.ToInt32(id);
    }

    public async Task<bool> UpdateAsync(int id, OperarioEtapa operarioEtapa)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand(@"UPDATE OperarioEtapa
                                         SET EtapaId = @EtapaId,
                                             OperarioId = @OperarioId,
                                             Duracion = @Duracion
                                         WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@EtapaId", operarioEtapa.EtapaId);
        cmd.Parameters.AddWithValue("@OperarioId", operarioEtapa.OperarioId);
        cmd.Parameters.AddWithValue("@Duracion", operarioEtapa.Duracion);
        await conn.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    private static OperarioEtapa Map(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(0),
        EtapaId = reader.GetInt32(1),
        OperarioId = reader.GetInt32(2),
        Duracion = reader.GetDecimal(3)
    };
}

