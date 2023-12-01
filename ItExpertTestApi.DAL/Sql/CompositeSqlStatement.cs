using Dapper;

namespace ItExpertTestApi.DAL.Sql
{
    public class CompositeSqlStatement : ISqlStatement
    {
        public CompositeSqlStatement(
            string separator,
            params ISqlStatement[] statements)
            : this(separator, (IEnumerable<ISqlStatement>)statements) { }

        public CompositeSqlStatement(
            string separator,
            IEnumerable<ISqlStatement> statements)
        {
            Separator = separator;
            Statements = statements.ToList();
        }

        public string Separator { get; set; }

        public List<ISqlStatement> Statements { get; set; }

        public (string?, DynamicParameters?) Build()
        {
            List<string> statements = new();
            DynamicParameters? parameters = null;
            foreach (ISqlStatement statement in Statements)
            {
                (string? st, DynamicParameters? pars) = statement.Build();
                if (st != null)
                {
                    statements.Add($"({st})");
                    if (pars != null)
                    {
                        parameters ??= new DynamicParameters();
                        parameters.AddDynamicParams(pars);
                    }
                }
            }
            if (statements.Count == 0)
            {
                return (null, null);
            }
            string stComp = string.Join(Separator, statements);
            return (stComp, parameters);
        }
    }
}
