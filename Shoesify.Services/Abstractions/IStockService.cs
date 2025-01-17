using Shoesify.Entities.Models;
using Shoesify.Services.Requests;
using Shoesify.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services.Abstractions
{
    public interface IStockService
    {
        public Task<GetStockOfInventoryResponse> GetStocks(GetStockOfInventoryRequest getStockOfInventoryRequest);
    }
}
