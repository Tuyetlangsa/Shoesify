using Microsoft.AspNetCore.Mvc;
using Shoesify.Apis.Common;
using Shoesify.Services.Abstractions;
using Shoesify.Services.Requests;

namespace Shoesify.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : Controller
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetStocks(string id)
        {
            try
            {
                var request = new GetStockOfInventoryRequest(id);
                var stock = await _stockService.GetStocks(request);
                return Ok(stock);
            }
            catch (Exception ex) {

                return BadRequest(new ApiResponse()
                {
                    Message = ex.Message
                });
            }
        }
    }
}
