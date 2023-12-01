namespace ItExpertTestApi.DAL.Sql
{
    public static class Sql
    {
        public const string AndSeparator = "\nAND ";
        public const string OrSeparator = "\nOR ";

        public static RawSqlStatement Raw(string statement)
        {
            return new RawSqlStatement(statement);
        }

        public static ParamSqlStatement<T> Param<T>(
            string name,
            T? value,
            Func<string, string> statement,
            string? nullStatement = null)
        {
            return new ParamSqlStatement<T>(name, value, statement, nullStatement);
        }

        public static CompositeSqlStatement And(params ISqlStatement[] statements)
        {
            return And((IEnumerable<ISqlStatement>)statements);
        }

        public static CompositeSqlStatement And(IEnumerable<ISqlStatement> statements)
        {
            return Composite(AndSeparator, statements, addParentheses: true);
        }

        public static CompositeSqlStatement Or(params ISqlStatement[] statements)
        {
            return Or((IEnumerable<ISqlStatement>)statements);
        }

        public static CompositeSqlStatement Or(IEnumerable<ISqlStatement> statements)
        {
            return Composite(OrSeparator, statements, addParentheses: true);
        }

        public static CompositeSqlStatement Composite(
            string separator,
            IEnumerable<ISqlStatement> statements,
            bool addParentheses = false)
        {
            return new CompositeSqlStatement(separator, statements, addParentheses);
        }

        public static WhereSqlStatement Where(
            params ISqlStatement[] statements)
        {
            return Where((IEnumerable<ISqlStatement>)statements);
        }

        public static WhereSqlStatement Where(
            IEnumerable<ISqlStatement> statements)
        {
            return new WhereSqlStatement(statements);
        }

        public static PaginationSqlStatement Pagination(
            int? page, int? pageSize)
        {
            return new PaginationSqlStatement(page, pageSize);
        }
    }
}
