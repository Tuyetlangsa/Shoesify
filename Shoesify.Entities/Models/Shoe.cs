using System;
using System.Collections.Generic;

namespace Shoesify.Entities.Models;

public partial class Shoe
{
    public string ShoeId { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public string? Name { get; set; }

    public double? Price { get; set; }

    public string? Brand { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ShoesDetail> ShoesDetails { get; set; } = new List<ShoesDetail>();
}
