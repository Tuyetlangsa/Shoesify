using Microsoft.AspNetCore.Mvc;
using Shoesify.Apis.Common;
using Shoesify.Entities.Models;
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

        // GET: api/inventory/ReadAll
        [HttpGet("ReadAll")]
        public async Task<ActionResult<List<Inventory>>> ReadAllInventory()
        {
            try
            {
                var inventories = await _inventoryService.ReadAllInventory();

                if (inventories == null || inventories.Count == 0)
                {
                    return NotFound("No inventory!");
                }

                return Ok(inventories);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse()
                {
                    Message = ex.Message
                });
            }
        }

        // PUT: api/inventory/Update
        [HttpPut("Update")]
        public async Task<ActionResult> UpdateInventory(UpdateInventoryRequest updatedInventory)
        {
            try
            {
                if (updatedInventory == null)
                {
                    return BadRequest("Data is not valid!");
                }

                bool result = await _inventoryService.UpdateInventory(updatedInventory);

                if (!result)
                {
                    return NotFound("Inventory not found!");
                }

                return Ok(new ApiResponse()
                {
                    Message = "Update Inventory Successfully!",
                    Payload = result
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse()
                {
                    Message = ex.Message
                });
            }
        }
        // PUT: api/inventory/Disable
        [HttpPut("Disable")]
        public async Task<ActionResult> DisableInventory(DisableInventoryRequest inventory)
        {
            try
            {
                if (inventory == null)
                {
                    return BadRequest("Data is not valid!");
                }

                bool result = await _inventoryService.DisableInventory(inventory);

                if (!result)
                {
                    return NotFound("Inventory not found!");
                }

                return Ok(new ApiResponse()
                {
                    Message = "Disable Inventory Successfully!",
                    Payload = result
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse()
                {
                    Message = ex.Message
                });
            }
        }

    }
}
