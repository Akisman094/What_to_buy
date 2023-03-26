using WhatToBuy.Context.Repositories;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Context.Entities;
using AutoMapper;

namespace WhatToBuy.Services.Items;

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public ItemService(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Item> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        ProcessException.ThrowIf(() =>item is null,$"Item with id {id} not found.");

        return item;
    }

    public async Task<IEnumerable<ItemModel>> GetAllItemsAsync()
    {
        var items = await _repository.GetAllAsync();

        ProcessException.ThrowIf(() => items.Any(),"No items found.");

        return _mapper.Map<List<ItemModel>>(items);
    }

    public async Task<ItemModel> CreateItemAsync(ItemAddModel itemModel)
    {
        ProcessException.ThrowIf(() => itemModel is null, "Cannot create null item.");

        var item = _mapper.Map<Item>(itemModel);
        await _repository.AddAsync(item);

        return _mapper.Map<ItemModel>(item);
    }

    public async Task UpdateItemAsync(int id, ItemUpdateModel itemModel)
    {
        ProcessException.ThrowIf(() => itemModel is null, "Cannot update null item.");

        var existingItem = await _repository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => existingItem is null, $"Item with id {id} not found.");

        _mapper.Map(existingItem, itemModel);

        await _repository.UpdateAsync(existingItem);
    }

    public async Task DeleteItemAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => item is null, $"Item with id {id} not found.");

        await _repository.DeleteAsync(item);
    }
}

