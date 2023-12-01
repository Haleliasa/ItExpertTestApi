using System.Data;

namespace ItExpertTestApi.DAL.DbConnectionProviders
{
    public interface IDbConnectionProvider
    {
        Task<IDbConnection> ConnectAsync();
    }
}
