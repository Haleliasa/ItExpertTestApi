using Dapper;

namespace ItExpertTestApi.DAL.Sql
{
    public class PaginationSqlStatement : ISqlStatement
    {
        public PaginationSqlStatement(int? page, int? pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int? Page { get; set; }

        public int? PageSize { get; set; }

        public (string?, DynamicParameters?) Build()
        {
            if (Page == null
                || Page < 1
                || PageSize == null
                || PageSize < 1)
            {
                return (null, null);
            }
            DynamicParameters parameters = new();
            parameters.Add("@pageSize", PageSize);
            string statement = "LIMIT @pageSize";
            if (Page > 1)
            {
                parameters.Add("@page", PageSize * (Page - 1));
                statement += " OFFSET @page";
            }
            return (statement, parameters);
        }
    }
}
