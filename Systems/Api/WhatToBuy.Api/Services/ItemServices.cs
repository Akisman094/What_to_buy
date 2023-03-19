using WhatToBuy.Api.Repositories;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Api.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;

    public ItemService(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Item> GetItemAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Item>> GetAllItemsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Item> CreateItemAsync(Item item)
    {
        await _repository.AddAsync(item);
        return item;
    }

    public async Task UpdateItemAsync(Item item)
    {
        await _repository.UpdateAsync(item);
    }

    public async Task DeleteItemAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(item);
    }
}
