using System.Data.SqlClient;
using TestExam.Domain;

namespace TestExam.Repositories;

public class ClienteRepository(string connectionString) : SqlRepositoryBase(connectionString), IClienteRepository
{
    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, Nombre, Apellido, Documento, Email, Telefono, DireccionId FROM Cliente", conn);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        var result = new List<Cliente>();
        while (await reader.ReadAsync())
        {
            result.Add(MapCliente(reader));
        }

        return result;
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, Nombre, Apellido, Documento, Email, Telefono, DireccionId FROM Cliente WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;

        return MapCliente(reader);
    }

    public async Task<int> AddAsync(Cliente cliente)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand(@"INSERT INTO Cliente (Nombre, Apellido, Documento, Email, Telefono, DireccionId)
                                         OUTPUT INSERTED.Id
                                         VALUES (@Nombre, @Apellido, @Documento, @Email, @Telefono, @DireccionId)", conn);
        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
        cmd.Parameters.AddWithValue("@Documento", cliente.Documento);
        cmd.Parameters.AddWithValue("@Email", cliente.Email);
        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
        cmd.Parameters.AddWithValue("@DireccionId", cliente.DireccionId);
        await conn.OpenAsync();
        var id = await cmd.ExecuteScalarAsync();
        return Convert.ToInt32(id);
    }

    public async Task<bool> UpdateAsync(int id, Cliente cliente)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand(@"UPDATE Cliente
                                         SET Nombre = @Nombre,
                                             Apellido = @Apellido,
                                             Documento = @Documento,
                                             Email = @Email,
                                             Telefono = @Telefono,
                                             DireccionId = @DireccionId
                                         WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
        cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
        cmd.Parameters.AddWithValue("@Documento", cliente.Documento);
        cmd.Parameters.AddWithValue("@Email", cliente.Email);
        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
        cmd.Parameters.AddWithValue("@DireccionId", cliente.DireccionId);
        await conn.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }

    private static Cliente MapCliente(SqlDataReader reader) => new()
    {
        Id = reader.GetInt32(0),
        Nombre = reader.GetString(1),
        Apellido = reader.GetString(2),
        Documento = reader.GetString(3),
        Email = reader.GetString(4),
        Telefono = reader.GetString(5),
        DireccionId = reader.GetInt32(6)
    };
}

