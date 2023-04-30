using System.Security.Claims;

namespace WhatToBuy.Services.ShoppingLists;
public interface IShoppingListService
{
    Task<ShoppingListModel> CreateAsync(ClaimsPrincipal user, AddShoppingListModel shoppingListModel);
    Task DeleteAsync(int id);
    Task<IEnumerable<ShoppingListModel>> GetAllAsync();
    Task<ShoppingListModel> GetByIdAsync(int id);
    Task<string> GetShoppingListBodyById(int id, string receiverName);
    Task<bool> IsAuthorized(ClaimsPrincipal user, int shopId);
    Task<ShoppingListModel> UpdateAsync(int id, ShoppingListUpdateModel updatedModel);
}