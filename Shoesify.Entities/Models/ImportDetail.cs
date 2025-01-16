using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shoesify.Entities.Models;

public partial class ImportDetail
{
    public string ImportId { get; set; } = null!;

    public string ShoeDetailId { get; set; } = null!;

    public int? Quantity { get; set; }
    
    [JsonIgnore]
    public virtual Import Import { get; set; } = null!;

    public virtual ShoesDetail ShoeDetail { get; set; } = null!;
}
