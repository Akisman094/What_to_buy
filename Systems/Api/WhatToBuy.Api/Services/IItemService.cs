using WhatToBuy.Context.Entities;

namespace WhatToBuy.Api.Services;

public interface IItemService
{
    Task<Item> GetItemAsync(int id);
    Task<IEnumerable<Item>> GetAllItemsAsync();
    Task<Item> CreateItemAsync(Item item);
    Task UpdateItemAsync(Item item);
    Task DeleteItemAsync(int id);
}