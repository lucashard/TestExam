using System.Data.SqlClient;
using TestExam.Domain;

namespace TestExam.Repositories;

public class DireccionRepository(string connectionString) : SqlRepositoryBase(connectionString), IDireccionRepository
{
    public async Task<IEnumerable<Direccion>> GetAllAsync()
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, Nombre, Altura FROM Direccion", conn);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        var result = new List<Direccion>();
        while (await reader.ReadAsync())
        {
            result.Add(new Direccion
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Altura = reader.GetInt32(2)
            });
        }

        return result;
    }

    public async Task<Direccion?> GetByIdAsync(int id)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, Nombre, Altura FROM Direccion WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;

        return new Direccion
        {
            Id = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            Altura = reader.GetInt32(2)
        };
    }

    public async Task<int> AddAsync(Direccion direccion)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("INSERT INTO Direccion (Nombre, Altura) OUTPUT INSERTED.Id VALUES (@Nombre, @Altura)", conn);
        cmd.Parameters.AddWithValue("@Nombre", direccion.Nombre);
        cmd.Parameters.AddWithValue("@Altura", direccion.Altura);
        await conn.OpenAsync();
        var id = await cmd.ExecuteScalarAsync();
        return Convert.ToInt32(id);
    }

    public async Task<bool> UpdateAsync(int id, Direccion direccion)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("UPDATE Direccion SET Nombre = @Nombre, Altura = @Altura WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@Nombre", direccion.Nombre);
        cmd.Parameters.AddWithValue("@Altura", direccion.Altura);
        await conn.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }
}

