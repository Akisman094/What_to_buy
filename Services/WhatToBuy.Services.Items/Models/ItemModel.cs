using AutoMapper;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Items;

public class ItemModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
}

public class ItemModelProfile : Profile
{
    public ItemModelProfile()
    {
        CreateMap<Item, ItemModel>();
    }
}