using AutoMapper;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.ShoppingLists;

public class AddShoppingListModel
{
    public string Name { get; set; }
    public int FamilyId { get; set; }
}

public class AddShoppingListModelProfile : Profile
{
    public AddShoppingListModelProfile()
    {
        CreateMap<AddShoppingListModel, ShoppingList>();
    }
}
