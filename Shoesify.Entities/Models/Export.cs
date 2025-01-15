using System;
using System.Collections.Generic;

namespace Shoesify.Entities.Models;

public partial class Export
{
    public string ExportId { get; set; } = null!;

    public string? InventoryId { get; set; }

    public DateOnly? ExportDate { get; set; }

    public virtual ICollection<ExportDetail> ExportDetails { get; set; } = new List<ExportDetail>();

    public virtual Inventory? Inventory { get; set; }
}
