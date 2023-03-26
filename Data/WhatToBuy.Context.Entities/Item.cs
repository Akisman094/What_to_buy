namespace WhatToBuy.Context.Entities;

/// <summary>
/// Item entity, used to form a shopping list
/// </summary>
public class Item : BaseEntity
{
    public String Name { get; set; }
    public int Amount { get; set; }

    public int? ShoppingListId { get; set; }
    public virtual ShoppingList ShoppingList { get; set; }
}
