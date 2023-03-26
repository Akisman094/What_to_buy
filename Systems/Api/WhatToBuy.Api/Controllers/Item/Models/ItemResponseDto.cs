using AutoMapper;
using WhatToBuy.Services.Items;

namespace WhatToBuy.Api.Controllers.Models;

public class ItemResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Amount { get; set; }
}

public class ItemResponseDtoProfile : Profile
{
    public ItemResponseDtoProfile()
    {
        CreateMap<ItemModel, ItemResponseDto>();
    }
}