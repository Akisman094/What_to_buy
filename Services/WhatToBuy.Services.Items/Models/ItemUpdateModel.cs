using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Items;

public class ItemUpdateModel
{
    public string Name { get; set; }
    public int Amount { get; set; }
}

public class ItemUpdateModelProfile : Profile
{
    public ItemUpdateModelProfile()
    {
        CreateMap<ItemUpdateModel, Item >();
    }
}
