using Dapper;

namespace ItExpertTestApi.DAL.Sql
{
    public class SimpleSqlStatement : ISqlStatement
    {
        public SimpleSqlStatement(string statement)
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
