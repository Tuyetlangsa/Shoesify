using System;
using System.Collections.Generic;

namespace Shoesify.Entities.Models;

public partial class Supplier
{
    public string SupplierId { get; set; } = null!;

    public string? SupplierName { get; set; }

    public string? SupplierAddress { get; set; }

    public string? SupplierPhone { get; set; }

    public string? SupplierEmail { get; set; }

    public virtual ICollection<Import> Imports { get; set; } = new List<Import>();
}
