using ItExpertTestApi.DAL.Models;

namespace ItExpertTestApi.Items
{
    public static class ItemDtoExtensions
    {
        public static GetItemsOptions ToOptions(this GetItemsParams itemsParams)
        {
            return new GetItemsOptions(
                Code: itemsParams.Code,
                CodeFrom: itemsParams.CodeFrom,
                CodeTo: itemsParams.CodeTo,
                Value: itemsParams.Value,
                ValueContains: itemsParams.ValueContains,
                Page: itemsParams.Page,
                PageSize: itemsParams.PageSize);
        }

        public static ItemOut ToOut(this Item model)
        {
            return new ItemOut(
                model.Order ?? -1,
                model.Code,
                model.Value);
        }

        public static Item ToModel(this ItemIn itemIn)
        {
            return new Item
            {
                Code = itemIn.Code,
                Value = itemIn.Value
            };
        }
    }
}
