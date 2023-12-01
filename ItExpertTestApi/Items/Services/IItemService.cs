using ItExpertTestApi.DAL.Models;

namespace ItExpertTestApi.Items
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItems(GetItemsOptions? options = null);

        Task SetItems(IEnumerable<Item> items);
    }
}
