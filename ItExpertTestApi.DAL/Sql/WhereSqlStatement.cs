using Dapper;

namespace ItExpertTestApi.DAL.Sql
{
    public class WhereSqlStatement : ISqlStatement
    {
        public WhereSqlStatement(params ISqlStatement[] statements)
            : this((IEnumerable<ISqlStatement>)statements) { }

        public WhereSqlStatement(IEnumerable<ISqlStatement> statements)
        {
            Statements = statements.ToList();
        }

        public List<ISqlStatement> Statements { get; set; }

        public (string?, DynamicParameters?) Build()
        {
            ISqlStatement statement = Sql.And(Statements);
            (string? st, DynamicParameters? parameters) = statement.Build();
            if (st == null)
            {
                return (null, null);
            }
            return ($"WHERE {st}", parameters);
        }
    }
}
