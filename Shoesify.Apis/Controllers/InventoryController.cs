using Microsoft.AspNetCore.Mvc;
using Shoesify.Apis.Common;
using Shoesify.Services.Abstractions;
using Shoesify.Services.Requests;

namespace Shoesify.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : Controller
    {

        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // implement api create an inventory
        [HttpPost("inventory/create")]
        public async Task<IActionResult> CreateInventory(CreateInventoryRequest request)
        {
            try
            {
                var isCreated = await _inventoryService.CreateAnInventory(request);

                if (isCreated > 0)
                    return Ok(new ApiResponse()
                    {
                        Message = "Inventory created successfully!"
                    });

                return BadRequest(new ApiResponse() { Message = "Failed to create inventory." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse() { Message = ex.Message });
            }
        }
    }
}
