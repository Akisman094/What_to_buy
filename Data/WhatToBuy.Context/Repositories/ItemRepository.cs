using Microsoft.EntityFrameworkCore;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Context.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly MainDbContext _context;
    private readonly DbSet<Item> _items;

    public ItemRepository(MainDbContext context)
    {
        _context = context;
        _items = context.Set<Item>();
    }

    public async Task<Item> GetByIdAsync(int id)
    {
        var items = _context.Set<Item>().Include(x => x.ShoppingList);
        var itemsQuery = from s in items where s.Id == id select s;
        var item = await itemsQuery.FirstOrDefaultAsync();
        return item;
    }

    public async Task<Item> GetByIdAndNavigPropsAsync(int id)
    {
        var items = _context.Set<Item>().Include(x => x.ShoppingList).ThenInclude(x => x.Family);
        var itemsQuery = from s in items where s.Id == id select s;
        var item = await itemsQuery.FirstOrDefaultAsync();
        return item;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _items.Include(x => x.ShoppingList).ToListAsync();
    }

    public async Task AddAsync(Item item)
    {
        await _items.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Item item)
    {
        _items.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Item item)
    {
        _items.Remove(item);
        await _context.SaveChangesAsync();
    }
}
