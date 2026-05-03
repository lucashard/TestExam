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
        using var cmd = new SqlCommand(@"INSERT INTO MateriaPrima (Nombre, Descripcion, Cantidad)
                                         OUTPUT INSERTED.Id
                                         VALUES (@Nombre, @Descripcion, @Cantidad)", conn);
        cmd.Parameters.AddWithValue("@Nombre", materiaPrima.Nombre);
        cmd.Parameters.AddWithValue("@Descripcion", materiaPrima.Descripcion);
        cmd.Parameters.AddWithValue("@Cantidad", materiaPrima.Cantidad);
        await conn.OpenAsync();
        var insertedId = await cmd.ExecuteScalarAsync();

        if (insertedId is null)
        {
            throw new InvalidOperationException("No se pudo obtener el identificador de la materia prima insertada.");
        }

        return Convert.ToInt32(insertedId);
    }

    public async Task<IEnumerable<MateriaPrima>> GetAllAsync(int? id = null, string? nombre = null, string? descripcion = null)
    {
        using var conn = new SqlConnection(_connectionString);
        var query = "SELECT Id, Nombre, Descripcion, Cantidad FROM MateriaPrima WHERE 1=1";
        if (id.HasValue) query += " AND Id = @Id";
        if (nombre != null) query += " AND Nombre LIKE @Nombre";
        if (descripcion != null) query += " AND Descripcion LIKE @Descripcion";

        using var cmd = new SqlCommand(query, conn);
        if (id.HasValue) cmd.Parameters.AddWithValue("@Id", id.Value);
        if (nombre != null) cmd.Parameters.AddWithValue("@Nombre", $"%{nombre}%");
        if (descripcion != null) cmd.Parameters.AddWithValue("@Descripcion", $"%{descripcion}%");

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        var result = new List<MateriaPrima>();
        while (await reader.ReadAsync())
        {
            result.Add(new MateriaPrima
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Descripcion = reader.GetString(2),
                Cantidad = reader.GetDecimal(3)
            });
        }

        return result;
    }

    public async Task<MateriaPrima?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand("SELECT Id, Nombre, Descripcion, Cantidad FROM MateriaPrima WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;

        return new MateriaPrima
        {
            Id = reader.GetInt32(0),
            Nombre = reader.GetString(1),
            Descripcion = reader.GetString(2),
            Cantidad = reader.GetDecimal(3)
        };
    }

    public async Task<bool> UpdateAsync(int id, MateriaPrima materiaPrima)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(@"UPDATE MateriaPrima
                                         SET Nombre = @Nombre,
                                             Descripcion = @Descripcion,
                                             Cantidad = @Cantidad
                                         WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@Nombre", materiaPrima.Nombre);
        cmd.Parameters.AddWithValue("@Descripcion", materiaPrima.Descripcion);
        cmd.Parameters.AddWithValue("@Cantidad", materiaPrima.Cantidad);
        await conn.OpenAsync();
        return await cmd.ExecuteNonQueryAsync() > 0;
    }
}
