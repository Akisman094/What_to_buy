using AutoMapper;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Context.Repositories;

namespace WhatToBuy.Services.ShoppingLists;
public class ShoppingListService : IShoppingListService
{
    private readonly IMapper _mapper;
    private readonly IShoppingListRepository _shoppingListRepository;

    public ShoppingListService(IMapper mapper, IShoppingListRepository shoppingListRepository)
    {
        _mapper = mapper;
        _shoppingListRepository = shoppingListRepository;
    }

    public async Task<IEnumerable<ShoppingListModel>> GetAllAsync()
    {
        var shoppingLists = await _shoppingListRepository.GetAllAsync();
        ProcessException.ThrowIf(() => shoppingLists.Any(), "No shopping lists avaliable");

        return _mapper.Map<IEnumerable<ShoppingListModel>>(shoppingLists);
    }

    public async Task<ShoppingListModel> GetByIdAsync(int id)
    {
        var shoppingList = await _shoppingListRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => shoppingList is null, $"Shopping list with id {id} not found.");

        return _mapper.Map<ShoppingListModel>(shoppingList);
    }

    public async Task<ShoppingListModel> CreateAsync(AddShoppingListModel shoppingList)
    {
        ProcessException.ThrowIf(() => shoppingList is null, "Shopping list cannot be null.");
        await _shoppingListRepository.CreateAsync(shoppingList);

        return _mapper.Map<ShoppingListModel>(shoppingList);
    }

    public async Task<ShoppingListModel> UpdateAsync(int id, ShoppingListUpdateModel updatedModel)
    {
        var shoppingList = await _shoppingListRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => shoppingList is null, $"Shopping list with id {id} not found.");

        shoppingList = _mapper.Map(updatedModel, shoppingList);
        await _shoppingListRepository.UpdateAsync(shoppingList);
        return shoppingList;
    }

    public async Task DeleteAsync(int id)
    {
        var shoppingList = await _shoppingListRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => shoppingList is null, $"Shopping list with id {id} not found.");

        await _shoppingListRepository.DeleteAsync(shoppingList);
    }

}
