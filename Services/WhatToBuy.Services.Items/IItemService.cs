using System.Security.Claims;

namespace WhatToBuy.Services.Items;
public interface IItemService
{
    Task<ItemModel> CreateItemAsync(ItemAddModel itemModel);
    Task DeleteItemAsync(int id);
    Task<IEnumerable<ItemModel>> GetAllItemsAsync();
    Task<ItemModel> GetByIdAsync(int id);
    Task<bool> IsAuthorized(ClaimsPrincipal user, int itemId);
    Task UpdateItemAsync(int id, ItemUpdateModel itemModel);
}