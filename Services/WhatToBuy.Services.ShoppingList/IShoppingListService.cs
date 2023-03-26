namespace WhatToBuy.Services.ShoppingLists;

public interface IShoppingListService
{
    Task<ShoppingListModel> CreateAsync(AddShoppingListModel shoppingList);
    Task DeleteAsync(int id);
    Task<IEnumerable<ShoppingListModel>> GetAllAsync();
    Task<ShoppingListModel> GetByIdAsync(int id);
    Task<ShoppingListModel> UpdateAsync(int id, ShoppingListUpdateModel updatedModel);
}