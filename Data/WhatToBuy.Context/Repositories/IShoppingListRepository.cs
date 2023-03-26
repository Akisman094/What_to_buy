using WhatToBuy.Context.Entities;

namespace WhatToBuy.Context.Repositories;
public interface IShoppingListRepository
{
    Task AddAsync(ShoppingList shoppingList);
    Task DeleteAsync(ShoppingList shoppingList);
    Task<IEnumerable<ShoppingList>> GetAllAsync();
    Task<ShoppingList> GetByIdAsync(int id);
    Task UpdateAsync(ShoppingList shoppingList);
}