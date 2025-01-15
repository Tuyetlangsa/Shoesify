using System;
using System.Collections.Generic;

namespace Shoesify.Entities.Models;

public partial class Stock
{
    public string InventoryId { get; set; } = null!;

    public string ShoeDetailId { get; set; } = null!;

    public int? Quantity { get; set; }

    public virtual Inventory Inventory { get; set; } = null!;

    public virtual ShoesDetail ShoeDetail { get; set; } = null!;
}
