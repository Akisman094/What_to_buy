using AutoMapper;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Items;

public class ItemAddModel
{
    public string Name { get; set; }
    public int Amount { get; set; }
}

public class ItemAddModelProfile : Profile
{
    public ItemAddModelProfile()
    {
        CreateMap<ItemAddModel, Item>();
    }
}