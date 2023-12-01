namespace ItExpertTestApi.Items
{
    public record class GetItemsParams(
        int? Code,
        int? CodeFrom,
        int? CodeTo,
        string? Value,
        string? ValueContains,
        int? Page,
        int? PageSize)
        : PaginationParams(Page, PageSize);
}
