using Dapper;

namespace ItExpertTestApi.DAL.Sql
{
    public class RawSqlStatement : ISqlStatement
    {
        public RawSqlStatement(string statement)
        {
            Statement = statement;
        }

        public string Statement { get; set; }

        public (string?, DynamicParameters?) Build()
        {
            return (Statement, null);
        }
    }
}
