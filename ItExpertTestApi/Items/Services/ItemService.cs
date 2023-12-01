using Dapper;
using ItExpertTestApi.DAL.DbConnectionProviders;
using ItExpertTestApi.DAL.Models;
using ItExpertTestApi.DAL.Sql;
using System.Data;

namespace ItExpertTestApi.Items
{
    public class ItemService : IItemService
    {
        private readonly IDbConnectionProvider _connectionProvider;

        public ItemService(IDbConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<GetItemsResult> GetItems(GetItemsOptions? options = null)
        {
            DynamicParameters parameters = new();
            string? filter = null;
            string? pagination = null;
            if (options != null)
            {
                (filter, DynamicParameters? filterPars) = SqlStatement.Where(
                    SqlStatement.Param("code", options.Code, p => $"code = {p}"),
                    SqlStatement.Param("codeFrom", options.CodeFrom, p => $"code >= {p}"),
                    SqlStatement.Param("codeTo", options.CodeTo, p => $"code <= {p}"),
                    SqlStatement.Param("value", options.Value, p => $"value = {p}"),
                    SqlStatement.Param("valueSub", options.ValueContains?.ToLower(),
                        p => $"position({p} in lower(value)) != 0"));
                parameters.AddDynamicParams(filterPars);

                (pagination, DynamicParameters? pagePars) = SqlStatement.Pagination(
                    options.Page,
                    options.PageSize);
                parameters.AddDynamicParams(pagePars);
            }

            string sql = $"""
                SELECT
                    "order",
                    code,
                    value,
                    COUNT(1) OVER() as totalCount
                FROM items
                {filter}
                {pagination}
                """;
            
            using IDbConnection connection = await _connectionProvider.ConnectAsync();
            IEnumerable<ItemTotalCount> items =
                await connection.QueryAsync<ItemTotalCount>(sql, parameters);
            int totalCount = items.FirstOrDefault()?.TotalCount ?? 0;

            return new GetItemsResult(items, totalCount);
        }

        public async Task SetItems(IEnumerable<Item> items)
        {
            List<Item> sortedItems = items.OrderBy(i => i.Code).ToList();
            string insertValues = string.Join(", ", sortedItems
                .Select((i, index) => $"({index + 1}, {i.Code}, \'{i.Value}\')"));

            using IDbConnection connection = await _connectionProvider.ConnectAsync();
            using IDbTransaction transaction = connection.BeginTransaction();
            await connection.ExecuteAsync(
                """DELETE FROM items""",
                transaction: transaction);
            await connection.ExecuteAsync(
                $"""INSERT INTO items ("order", code, value) VALUES {insertValues}""",
                transaction: transaction);
            transaction.Commit();

            for (int i = 0; i < sortedItems.Count; i++)
            {
                sortedItems[i].Order = i + 1;
            }
        }

        private class ItemTotalCount : Item
        {
            public int TotalCount { get; set; }
        }
    }
}
