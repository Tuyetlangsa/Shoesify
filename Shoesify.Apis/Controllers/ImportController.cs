using Microsoft.AspNetCore.Mvc;
using Shoesify.Apis.Common;
using Shoesify.Services.Requests;
using Shoesify.Apis.Common;
using Shoesify.Services.Abstractions;


namespace Shoesify.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : Controller
    {
        private readonly IImportService _service;

        public ImportController(IImportService service)
        {
            _service = service;
        }

        [HttpPost("/import/create")]
        public async Task<IActionResult> CreateImport([FromBody] ImportRequest importRequest)
        {
            try
            {
                var result = await _service.CreateImport(importRequest);
                if (result > 0)

                    return Ok(new ApiResponse()
                    {
                        Message = "Import created successfully",

                    });
                return BadRequest(new ApiResponse()
                {
                    Message = "Failed to create import"
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse() { Message = e.Message });
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImportByID(string id)
        {
            var result = await _service.GetImportByID(id);
            if (result == null)
                return BadRequest(new ApiResponse()
                {
                    Message = "Not found"
                });
            return Ok(result);


        }

        [HttpGet("/imports/inventory/{inventoryId}")]
        public async Task<IActionResult> GetAllImportByInventoryId(string inventoryId)
        {
            try
            {
                GetAllImportRequest request = new GetAllImportRequest(inventoryId);   
                var result = await _service.GetAllImportOfInventory(request);
                if (result == null || result.Count == 0)
                {
                    return NotFound(new ApiResponse()
                    {
                        Message = "No imports found for the given inventory ID"
                    });
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, new ApiResponse() { Message = e.Message });
            }
        }

    }
}
