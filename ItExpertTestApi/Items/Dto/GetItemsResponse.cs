namespace ItExpertTestApi.Items
{
    public record class GetItemsResponse(
        IEnumerable<ItemOut> Items,
        int TotalCount);
}
