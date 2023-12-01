using Dapper;

namespace ItExpertTestApi.DAL.Sql
{
    public static class SqlStatement
    {
        public const string AndSeparator = "\nAND ";
        public const string OrSeparator = "\nOR ";

        public static ISqlStatement Simple(string statement)
        {
            return new SimpleSqlStatement(statement);
        }

        public static ISqlStatement Param<T>(
            string name,
            T? value,
            Func<string, string> statement,
            bool notNull = true)
        {
            return new ParamSqlStatement<T>(name, value, statement, notNull);
        }

        public static ISqlStatement And(params ISqlStatement[] statements)
        {
            return And((IEnumerable<ISqlStatement>)statements);
        }

        public static ISqlStatement And(IEnumerable<ISqlStatement> statements)
        {
            return new CompositeSqlStatement(AndSeparator, statements);
        }

        public static ISqlStatement Or(params ISqlStatement[] statements)
        {
            return Or((IEnumerable<ISqlStatement>)statements);
        }

        public static ISqlStatement Or(IEnumerable<ISqlStatement> statements)
        {
            return new CompositeSqlStatement(OrSeparator, statements);
        }

        public static (string?, DynamicParameters?) Where(
            params ISqlStatement[] statements)
        {
            return Where((IEnumerable<ISqlStatement>)statements);
        }

        public static (string?, DynamicParameters?) Where(
            IEnumerable<ISqlStatement> statements)
        {
            ISqlStatement statement = And(statements);
            (string? st, DynamicParameters? parameters) = statement.Build();
            if (st == null)
            {
                return (null, null);
            }
            return ($"WHERE {st}", parameters);
        }

        public static (string?, DynamicParameters?) Pagination(
            int? page, int? pageSize)
        {
            if (page == null
                || page < 1
                || pageSize == null
                || pageSize < 1)
            {
                return (null, null);
            }
            DynamicParameters parameters = new();
            parameters.Add("@pageSize", pageSize);
            string statement = "LIMIT @pageSize";
            if (page > 1)
            {
                parameters.Add("@page", pageSize * (page - 1));
                statement += " OFFSET @page";
            }
            return (statement, parameters);
        }
    }
}
