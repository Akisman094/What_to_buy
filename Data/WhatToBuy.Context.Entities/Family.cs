namespace WhatToBuy.Context.Entities;

/// <summary>
/// Family Entity
/// </summary>
public class Family : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
}

