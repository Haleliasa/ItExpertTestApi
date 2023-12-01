namespace ItExpertTestApi.Items
{
    public record class GetItemsOptions(
        int? Code = null,
        int? CodeFrom = null,
        int? CodeTo = null,
        string? Value = null,
        string? ValueContains = null,
        int? Page = null,
        int? PageSize = null);
}
