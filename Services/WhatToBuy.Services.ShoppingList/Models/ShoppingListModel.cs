using AutoMapper;
using WhatToBuy.Context.Entities;
using WhatToBuy.Services.Items;

namespace WhatToBuy.Services.ShoppingLists;

public class ShoppingListModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int FamilyId { get; set; }
    public ICollection<string> Items { get; set; }
}

public class ShoppingListModelProfile : Profile
{
    public ShoppingListModelProfile()
    {
        CreateMap<ShoppingList, ShoppingListModel>()
            .AfterMap((s,d) => d.Items = new List<string>(s.Items.Select(x => x.Name)));
    }
}
