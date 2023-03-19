namespace WhatToBuy.Api.Controllers.ShoppingList.Models;

public class ShoppingListResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ItemResponseDto> Items { get; set; }
}
