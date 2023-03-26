using Microsoft.EntityFrameworkCore;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Context.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly DbContext _context;
    private readonly DbSet<Item> _items;

    public ItemRepository(DbContext context)
    {
        _context = context;
        _items = context.Set<Item>();
    }

    public async Task<Item> GetByIdAsync(int id)
    {
        return await _items.FindAsync(id);
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _items.ToListAsync();
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
