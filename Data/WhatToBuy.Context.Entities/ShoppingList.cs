using AutoMapper;

namespace WhatToBuy.Context.Entities;

public class ShoppingList : BaseEntity
{
    /// <summary>
    /// Shopping List
    /// </summary>
    public string Name { get; set; }
    
    public virtual IEnumerable<Item> Items { get; set; }

    public int? FamilyId { get; set; }
    public virtual Family Family { get; set; }
}