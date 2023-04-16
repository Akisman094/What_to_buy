using AutoMapper;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Context.Entities;
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

    public async Task<ShoppingListModel> CreateAsync(AddShoppingListModel shoppingListModel)
    {
        ProcessException.ThrowIf(() => shoppingListModel is null, "Shopping list cannot be null.");
        var shoppingList = _mapper.Map<ShoppingList>(shoppingListModel);
        await _shoppingListRepository.AddAsync(shoppingList);

        return _mapper.Map<ShoppingListModel>(shoppingList);
    }

    public async Task<ShoppingListModel> UpdateAsync(int id, ShoppingListUpdateModel updatedModel)
    {
        var shoppingList = await _shoppingListRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => shoppingList is null, $"Shopping list with id {id} not found.");

        shoppingList = _mapper.Map(updatedModel, shoppingList);
        await _shoppingListRepository.UpdateAsync(shoppingList);

        var responseModel = _mapper.Map<ShoppingListModel>(updatedModel);
        return responseModel;
    }

    public async Task DeleteAsync(int id)
    {
        var shoppingList = await _shoppingListRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => shoppingList is null, $"Shopping list with id {id} not found.");

        await _shoppingListRepository.DeleteAsync(shoppingList);
    }

}
