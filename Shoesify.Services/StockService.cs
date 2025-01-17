using Microsoft.EntityFrameworkCore;
using Shoesify.Entities.Models;
using Shoesify.Services.Abstractions;
using Shoesify.Services.Requests;
using Shoesify.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services
{
    public class StockService : IStockService
    {
        
        private readonly ShoesifyContext _context;

        public StockService( ShoesifyContext context)
        {
            
            _context = context;
        }

        public async Task<GetStockOfInventoryResponse> GetStocks(GetStockOfInventoryRequest getStockOfInventoryRequest)
        {
            //  var stocks = await _context.Stocks.Where(i => i.InventoryId == getStockOfInventoryRequest.inventoryId).Include(i=>i.ShoeDetail).ToListAsync();

            var stocks = await _context.Stocks
        .Where(i => i.InventoryId == getStockOfInventoryRequest.inventoryId)
        .Include(i => i.ShoeDetail)
        .ThenInclude(sd => sd.Shoe)
        .ToListAsync();

            var response = new GetStockOfInventoryResponse
            {
                InventoryId = getStockOfInventoryRequest.inventoryId,
                StockDetails = stocks.Select(stock => new StockDetails
                {
                    ShoesId = stock.ShoeDetailId,
                    Name = stock.ShoeDetail.Shoe.Name,
                    Size = stock.ShoeDetail.Size, 
                    ShoesDetailId = stock.ShoeDetailId,
                    Quantity = stock.Quantity 
                }).ToList()
            };

            return response;
        }
    }
}
