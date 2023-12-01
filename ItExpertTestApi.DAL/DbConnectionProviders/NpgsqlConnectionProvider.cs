using Npgsql;
using System.Data;

namespace ItExpertTestApi.DAL.DbConnectionProviders
{
    public class NpgsqlConnectionProvider : IDbConnectionProvider
    {
        private readonly string _connectionString;

        public NpgsqlConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> ConnectAsync()
        {
            NpgsqlConnection connection = new(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
