namespace WhatToBuy.Context.Entities;

using AutoMapper;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public string Name { get; set; }
    public UserStatus Status { get; set; }     

    public int? FamilyId { get; set; }
    public virtual Family Family { get; set; }
}
