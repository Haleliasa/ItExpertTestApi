using ItExpertTestApi.DAL.Models;

namespace ItExpertTestApi.Items
{
    public interface IItemService
    {
        Task<GetItemsResult> GetItems(GetItemsOptions? options = null);

        Task SetItems(IEnumerable<Item> items);
    }
}
