using System.Data.SqlClient;
using TestExam.Domain;

namespace TestExam.Repositories;

public class FacturaRepository(string connectionString) : SqlRepositoryBase(connectionString), IFacturaRepository
{
    public async Task<IEnumerable<Factura>> GetAllAsync()
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, ClienteId FROM Factura", conn);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        var result = new List<Factura>();
        while (await reader.ReadAsync())
        {
            result.Add(new Factura { Id = reader.GetInt32(0), ClienteId = reader.GetInt32(1) });
        }

        await reader.CloseAsync();

        foreach (var factura in result)
        {
            factura.Etapas = await GetEtapasByFacturaIdAsync(conn, null, factura.Id);
        }

        return result;
    }

    public async Task<Factura?> GetByIdAsync(int id)
    {
        using var conn = CreateConnection();
        using var cmd = new SqlCommand("SELECT Id, ClienteId FROM Factura WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;

        var factura = new Factura { Id = reader.GetInt32(0), ClienteId = reader.GetInt32(1) };
        await reader.CloseAsync();
        factura.Etapas = await GetEtapasByFacturaIdAsync(conn, null, factura.Id);

        return factura;
    }

    public async Task<int> AddAsync(Factura factura)
    {
        using var conn = CreateConnection();
        await conn.OpenAsync();
        using var tx = conn.BeginTransaction();
        using var cmd = new SqlCommand("INSERT INTO Factura (ClienteId) OUTPUT INSERTED.Id VALUES (@ClienteId)", conn);
        cmd.Transaction = tx;
        cmd.Parameters.AddWithValue("@ClienteId", factura.ClienteId);
        var id = await cmd.ExecuteScalarAsync();
        var facturaId = Convert.ToInt32(id);

        await ReplaceEtapasAsync(conn, tx, facturaId, factura.Etapas);
        tx.Commit();

        return facturaId;
    }

    public async Task<bool> UpdateAsync(int id, Factura factura)
    {
        using var conn = CreateConnection();
        await conn.OpenAsync();
        using var tx = conn.BeginTransaction();
        using var cmd = new SqlCommand("UPDATE Factura SET ClienteId = @ClienteId WHERE Id = @Id", conn);
        cmd.Transaction = tx;
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@ClienteId", factura.ClienteId);
        var updatedRows = await cmd.ExecuteNonQueryAsync();

        if (updatedRows <= 0)
        {
            tx.Rollback();
            return false;
        }

        await ReplaceEtapasAsync(conn, tx, id, factura.Etapas);
        tx.Commit();
        return true;
    }

    private static async Task<List<Etapas>> GetEtapasByFacturaIdAsync(SqlConnection conn, SqlTransaction? tx, int facturaId)
    {
        using var cmd = new SqlCommand(@"SELECT e.Id, e.Nombre
                                         FROM FacturaEtapa fe
                                         INNER JOIN Etapas e ON e.Id = fe.EtapaId
                                         WHERE fe.FacturaId = @FacturaId
                                         ORDER BY e.Id", conn);
        if (tx is not null)
        {
            cmd.Transaction = tx;
        }

        cmd.Parameters.AddWithValue("@FacturaId", facturaId);

        using var reader = await cmd.ExecuteReaderAsync();
        var etapas = new List<Etapas>();
        while (await reader.ReadAsync())
        {
            etapas.Add(new Etapas
            {
                Id = reader.GetInt32(0),
                Nombre = reader.GetString(1)
            });
        }

        return etapas;
    }

    private static async Task ReplaceEtapasAsync(SqlConnection conn, SqlTransaction tx, int facturaId, IEnumerable<Etapas>? etapas)
    {
        using var deleteCmd = new SqlCommand("DELETE FROM FacturaEtapa WHERE FacturaId = @FacturaId", conn, tx);
        deleteCmd.Parameters.AddWithValue("@FacturaId", facturaId);
        await deleteCmd.ExecuteNonQueryAsync();

        if (etapas is null)
        {
            return;
        }

        var etapasIds = etapas
            .Select(e => e.Id)
            .Where(id => id > 0)
            .Distinct()
            .ToList();

        foreach (var etapaId in etapasIds)
        {
            using var insertCmd = new SqlCommand("INSERT INTO FacturaEtapa (FacturaId, EtapaId) VALUES (@FacturaId, @EtapaId)", conn, tx);
            insertCmd.Parameters.AddWithValue("@FacturaId", facturaId);
            insertCmd.Parameters.AddWithValue("@EtapaId", etapaId);
            await insertCmd.ExecuteNonQueryAsync();
        }
    }
}

