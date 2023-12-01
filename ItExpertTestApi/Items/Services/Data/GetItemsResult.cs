using ItExpertTestApi.DAL.Models;

namespace ItExpertTestApi.Items
{
    public record class GetItemsResult(
        IEnumerable<Item> Items,
        int TotalCount);
}
