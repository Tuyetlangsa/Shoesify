using System;
using System.Collections.Generic;

namespace Shoesify.Entities.Models;

public partial class Category
{
    public string CategoryId { get; set; } = null!;

    public string? CategoryName { get; set; }

    public virtual ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
}
