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
        public async Task<ActionResult<GetItemsResponse>> GetItems(
            [FromQuery] GetItemsParams itemsParams)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            GetItemsResult result = await _service.GetItems(
                options: itemsParams.ToOptions());
            return result.ToResponse();
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
