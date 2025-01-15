using System;
using System.Collections.Generic;

namespace Shoesify.Entities.Models;

public partial class ExportDetail
{
    public string ExportId { get; set; } = null!;

    public string ShoeDetailId { get; set; } = null!;

    public int? Quantity { get; set; }

    public virtual Export Export { get; set; } = null!;

    public virtual ShoesDetail ShoeDetail { get; set; } = null!;
}
