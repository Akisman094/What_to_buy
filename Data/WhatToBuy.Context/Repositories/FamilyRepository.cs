namespace WhatToBuy.Context.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhatToBuy.Context.Entities;

public class FamilyRepository : IFamilyRepository
{
    private readonly MainDbContext _context;
    private readonly DbSet<Family> _families;

    public FamilyRepository(MainDbContext context)
    {
        _context = context;
        _families = context.Set<Family>();
    }

    public async Task<IEnumerable<Family>> GetAllAsync()
    {
        return await _families.Include(x => x.ShoppingLists).Include(x => x.Users).ToListAsync();
    }

    public async Task<Family> GetByIdAsync(int id)
    {
        var families = _context.Set<Family>().Include(x => x.ShoppingLists).Include(x => x.Users);
        var familiesQuery = from s in families where s.Id == id select s;
        var family = await familiesQuery.FirstOrDefaultAsync();
        return family;
    }

    public async Task AddAsync(Family entity)
    {
        await _families.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Family entity)
    {
        _families.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Family entity)
    {
        _families.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
