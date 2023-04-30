using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;
using WhatToBuy.Common.Exceptions;
using WhatToBuy.Common.Security;
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
        ProcessException.ThrowIf(() => !shoppingLists.Any(), StatusCodes.Status404NotFound, "No shopping lists avaliable");

        return _mapper.Map<IEnumerable<ShoppingList>, IEnumerable<ShoppingListModel>>(shoppingLists);
    }

    public async Task<ShoppingListModel> GetByIdAsync(int id)
    {
        var shoppingList = await _shoppingListRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => shoppingList is null, StatusCodes.Status404NotFound, $"Shopping list with id {id} not found.");

        return _mapper.Map<ShoppingListModel>(shoppingList);
    }

    public async Task<ShoppingListModel> CreateAsync(ClaimsPrincipal user, AddShoppingListModel shoppingListModel)
    {
        ProcessException.ThrowIf(() => shoppingListModel is null, "Shopping list cannot be null.");

        var familyId = user.FindFirstValue(AppClaims.FamilyIdClaim);
        shoppingListModel.FamilyId = int.Parse(familyId);

        var shoppingList = _mapper.Map<ShoppingList>(shoppingListModel);
        await _shoppingListRepository.AddAsync(shoppingList);

        return _mapper.Map<ShoppingListModel>(shoppingList);
    }

    public async Task<ShoppingListModel> UpdateAsync(int id, ShoppingListUpdateModel updatedModel)
    {
        var shoppingList = await _shoppingListRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => shoppingList is null, StatusCodes.Status404NotFound, $"Shopping list with id {id} not found.");

        shoppingList = _mapper.Map(updatedModel, shoppingList);
        await _shoppingListRepository.UpdateAsync(shoppingList);

        var responseModel = _mapper.Map<ShoppingListModel>(shoppingList);
        return responseModel;
    }

    public async Task DeleteAsync(int id)
    {
        var shoppingList = await _shoppingListRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => shoppingList is null, StatusCodes.Status404NotFound, $"Shopping list with id {id} not found.");

        await _shoppingListRepository.DeleteAsync(shoppingList);
    }
    
    public async Task<string> GetShoppingListBodyById(int id, string receiverName)
    {
        var shoppingList = await _shoppingListRepository.GetByIdAsync(id);
        ProcessException.ThrowIf(() => shoppingList is null, StatusCodes.Status404NotFound, $"Shopping list with id {id} not found.");

        var body = new StringBuilder();
        body.AppendLine($"<p>Dear Mr(s). {receiverName},<br>");
        body.AppendLine($"Please, review the list of products that need to be bought from the \"{shoppingList.Name}\" shopping list.<br>All items are listed below:");
        body.AppendLine("<ul style=\"list-style-type: square;\">");
        foreach(var item in shoppingList.Items)
        {
            body.AppendLine($"<li>{item.ToCustomString()}</li>");
        }
        body.AppendLine("</ul>");
        body.AppendLine("</p>");

        return body.ToString();
    }

    public async Task<bool> IsAuthorized(ClaimsPrincipal user, int shopId)
    {
        var tokenFamilyId = int.Parse(user.FindFirstValue(AppClaims.FamilyIdClaim));
        var shoppingList = await _shoppingListRepository.GetByIdAsync(shopId);
        var familyId = shoppingList.FamilyId;

        ProcessException.ThrowIf(() => tokenFamilyId != familyId, StatusCodes.Status401Unauthorized, $"Can't access shopping list with id:{shopId}, because it is not user's familie's");

        return true;
    }
}
