using AutoMapper;

namespace WhatToBuy.Context.Entities;

/// <summary>
/// Family Entity
/// </summary>
public class Family : BaseEntity
{
    public string Name { get; set; } = "New Family";
    public virtual IEnumerable<User> Users { get; set; }
    public virtual IEnumerable<ShoppingList> ShoppingLists { get; set; }

    public Family(string name) 
    { 
        Name = name;
    }
    public Family() { }
}
