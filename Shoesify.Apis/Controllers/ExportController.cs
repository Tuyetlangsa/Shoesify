using Microsoft.AspNetCore.Mvc;
using Shoesify.Apis.Common;
using Shoesify.Services.Abstractions;
using Shoesify.Services.Requests;

namespace Shoesify.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : Controller
    {
        private readonly IExportService _exportService;

        public ExportController(IExportService exportService)
        {
            _exportService = exportService;
        }

        // API: Create an Export
        [HttpPost("export/create")]
        public async Task<IActionResult> CreateExport(CreateExportRequest request)
        {
            try
            {
                var isCreated = await _exportService.CreateExport(request);

                if (isCreated > 0)
                {
                    return Ok(new ApiResponse()
                    {
                        Message = "Export created successfully!"
                    });
                }

                return BadRequest(new ApiResponse()
                {
                    Message = "Failed to create export."
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponse() { Message = ex.Message });
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

        // API: Read Export Details by Export ID
        [HttpGet("export/{exportId}")]
        public async Task<IActionResult> GetExportDetails(string exportId)
        {
            try
            {
                var export = await _exportService.ReadExport(exportId);

                if (export != null)
                {
                    return Ok(new ApiResponse()
                    {
                        Message = "Export details retrieved successfully!",
                        Payload = export
                    });
                }

                return NotFound(new ApiResponse()
                {
                    Message = $"Export with ID {exportId} not found."
                });
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
