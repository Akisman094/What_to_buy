using WhatToBuy.Context.Entities;

namespace WhatToBuy.Context.Repositories;
public interface IItemRepository
{
    Task AddAsync(Item item);
    Task DeleteAsync(Item item);
    Task<IEnumerable<Item>> GetAllAsync();
    Task<Item> GetByIdAndNavigPropsAsync(int id);
    Task<Item> GetByIdAsync(int id);
    Task UpdateAsync(Item item);
}