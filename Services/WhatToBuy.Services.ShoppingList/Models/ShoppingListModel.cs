using AutoMapper;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.ShoppingLists;

public class ShoppingListModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int FamilyId { get; set; }
    public IEnumerable<string> Items { get; set; }
}

public class ShoppingListModelProfile : Profile
{
    public ShoppingListModelProfile()
    {
        CreateMap<ShoppingList, ShoppingListModel>();
        CreateMap<Item, string>().ConvertUsing(x => $"{x.Name} x {x.Amount}");
    }
}
