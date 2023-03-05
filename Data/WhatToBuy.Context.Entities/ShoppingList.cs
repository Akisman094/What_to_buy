namespace WhatToBuy.Context.Entities;

public class ShoppingList : BaseEntity
{
    /// <summary>
    /// Shopping List
    /// </summary>
    public virtual ICollection<Item> Items { get; set; }

    public int? FamilyId { get; set; }
    public virtual Family Family { get; set; }
}
