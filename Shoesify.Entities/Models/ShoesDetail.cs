using System;
using System.Collections.Generic;

namespace Shoesify.Entities.Models;

public partial class ShoesDetail
{
    public string ShoeDetailId { get; set; } = null!;

    public string? ShoeId { get; set; }

    public int? Size { get; set; }

    public string? Color { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<ExportDetail> ExportDetails { get; set; } = new List<ExportDetail>();

    public virtual ICollection<ImportDetail> ImportDetails { get; set; } = new List<ImportDetail>();

    public virtual Shoe? Shoe { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
