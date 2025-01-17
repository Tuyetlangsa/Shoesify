using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoesify.Services.Responses
{
    public class GetStockOfInventoryResponse
    {
        public string InventoryId { get; set; }

        public List<StockDetails> StockDetails { get; set; } = new List<StockDetails>();

    }

    public class StockDetails
    {
        public string ShoesId { get; set; }
        public string Name { get; set; }
        public int? Size { get; set; }
        public string ShoesDetailId { get; set; }
        public int? Quantity { get; set; }
    }
}
