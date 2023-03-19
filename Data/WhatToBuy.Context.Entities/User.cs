namespace WhatToBuy.Context.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public string name { get; set; }
    public string email { get; set; }

    public int? FamilyId { get; set; }
    public virtual Family Family { get; set; }
}
