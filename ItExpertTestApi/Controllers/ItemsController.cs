using ItExpertTestApi.DAL.Models;
using ItExpertTestApi.Items;
using Microsoft.AspNetCore.Mvc;

namespace ItExpertTestApi.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _service;

        public ItemsController(IItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemOut>>> GetItems(
            [FromQuery] GetItemsParams itemsParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IEnumerable<Item> items = await _service.GetItems(
                options: itemsParams.ToOptions());
            List<ItemOut> itemsOut = items.Select(item => item.ToOut()).ToList();
            return itemsOut;
        }

        [HttpPost]
        public async Task<IActionResult> SetItems(
            [FromBody] IEnumerable<ItemIn> items)
        {
            List<Item> itemModels = items.Select(item => item.ToModel()).ToList();
            await _service.SetItems(itemModels);
            return Ok();
        }
    }
}
