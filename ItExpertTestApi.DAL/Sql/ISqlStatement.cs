using Dapper;

namespace ItExpertTestApi.DAL.Sql
{
    public interface ISqlStatement
    {
        (string?, DynamicParameters?) Build();
    }
}
