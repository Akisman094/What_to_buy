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
        var shoppingLists = await _dbContext.Set<ShoppingList>().Include(x => x.Family).Include(x => x.Items).ToListAsync();
        return shoppingLists;
    }

    public async Task<ShoppingList> GetByIdAsync(int id)
    {
        var shoppingLists = _dbContext.Set<ShoppingList>().Include(x => x.Family).Include(x => x.Items);
        var shoppingListQuery = from s in shoppingLists where s.Id == id select s;
        var shoppingList = await shoppingListQuery.FirstOrDefaultAsync();
        return shoppingList;
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
