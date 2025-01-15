using System;
using System.Collections.Generic;

namespace Shoesify.Entities.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public string? Role { get; set; }

    public bool? Status { get; set; }

    public virtual Inventory? Inventory { get; set; }
}
