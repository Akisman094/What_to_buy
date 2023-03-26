namespace WhatToBuy.Context.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }

    public int? FamilyId { get; set; }
    public virtual Family Family { get; set; }
}
