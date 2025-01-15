using System;
using System.Collections.Generic;

namespace Shoesify.Entities.Models;

public partial class Inventory
{
    public string InventoryId { get; set; } = null!;

    public string? UserId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Export> Exports { get; set; } = new List<Export>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual User? User { get; set; }
}
