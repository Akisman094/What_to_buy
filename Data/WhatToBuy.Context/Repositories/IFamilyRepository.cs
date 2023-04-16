using WhatToBuy.Context.Entities;

namespace WhatToBuy.Context.Repositories;
public interface IFamilyRepository
{
    Task AddAsync(Family entity);
    Task DeleteAsync(Family entity);
    Task<IEnumerable<Family>> GetAllAsync();
    Task<Family> GetByIdAsync(int id);
    Task UpdateAsync(Family entity);
}