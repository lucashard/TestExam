using System.Data.SqlClient;

namespace TestExam.Repositories;

public abstract class SqlRepositoryBase(string connectionString)
{
    protected SqlConnection CreateConnection() => new(connectionString);
}
