using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Items;
public interface IItemService
{
    Task<ItemModel> CreateItemAsync(ItemAddModel itemModel);
    Task DeleteItemAsync(int id);
    Task<IEnumerable<ItemModel>> GetAllItemsAsync();
    Task<Item> GetByIdAsync(int id);
    Task UpdateItemAsync(int id, ItemUpdateModel itemModel);
}