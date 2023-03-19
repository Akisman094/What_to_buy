namespace WhatToBuy.Api.Controllers.ShoppingList.Models;

public class ShoppingListRequestDto
{
    public string Name { get; set; }
    public List<int> ItemIds { get; set; }
}
