using WhatToBuy.Context.Repositories;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Context.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WhatToBuy.Common.Security;

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

    public async Task<ItemModel> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        ProcessException.ThrowIf(() => item is null, $"Item with id {id} not found.");

        var itemModel = _mapper.Map<ItemModel>(item);
        return itemModel;
    }

    public async Task<IEnumerable<ItemModel>> GetAllItemsAsync()
    {
        var items = await _repository.GetAllAsync();

        ProcessException.ThrowIf(() => !items.Any(), "No items found.");

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

        if (itemModel.Name is not null)
            existingItem.Name = itemModel.Name;
        if (itemModel.Amount is not null)
            existingItem.Amount = (int)itemModel.Amount;

        await _repository.UpdateAsync(existingItem);
    }

    public async Task DeleteItemAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => item is null, $"Item with id {id} not found.");

        await _repository.DeleteAsync(item);
    }

    public async Task<bool> IsAuthorized(ClaimsPrincipal user, int itemId)
    {
        var tokenFamilyId = int.Parse(user.FindFirstValue(AppClaims.FamilyIdClaim));
        var item = await GetByIdAndNavigPropsAsync(itemId);
        var familyId = item.ShoppingList.FamilyId;

        ProcessException.ThrowIf(() => tokenFamilyId != familyId, StatusCodes.Status401Unauthorized, $"Can't access item with id:{itemId}, because it is not user's familie's item");

        return true;
    }

    private async Task<Item> GetByIdAndNavigPropsAsync(int id)
    {
        var item = await _repository.GetByIdAndNavigPropsAsync(id);

        ProcessException.ThrowIf(() => item is null, $"Item with id {id} not found.");

        return item;
    }
}