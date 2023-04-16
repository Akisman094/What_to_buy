using Microsoft.EntityFrameworkCore;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Context.Repositories;

public class ShoppingListRepository : IShoppingListRepository
{
    private readonly MainDbContext _dbContext;

    public ShoppingListRepository(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ShoppingList>> GetAllAsync()
    {
        return await _dbContext.Set<ShoppingList>().ToListAsync();
    }

    public async Task<ShoppingList> GetByIdAsync(int id)
    {
        return await _dbContext.Set<ShoppingList>().FindAsync(id);
    }

    public async Task AddAsync(ShoppingList shoppingList)
    {
        await _dbContext.Set<ShoppingList>().AddAsync(shoppingList);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ShoppingList shoppingList)
    {
        _dbContext.Set<ShoppingList>().Update(shoppingList);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ShoppingList shoppingList)
    {
        _dbContext.Set<ShoppingList>().Remove(shoppingList);
        await _dbContext.SaveChangesAsync();
    }
}
