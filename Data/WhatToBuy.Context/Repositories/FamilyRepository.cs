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
        return await _families.ToListAsync();
    }

    public async Task<Family> GetByIdAsync(int id)
    {
        return await _families.FindAsync(id);
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
