using System;
using System.Collections.Generic;

namespace Shoesify.Entities.Models;

public partial class Import
{
    public string ImportId { get; set; } = null!;

    public string? SupplierId { get; set; }

    public DateOnly? ImportDate { get; set; }

    public string? InventoryId { get; set; }

    public virtual ICollection<ImportDetail> ImportDetails { get; set; } = new List<ImportDetail>();

    public virtual Inventory? Inventory { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
