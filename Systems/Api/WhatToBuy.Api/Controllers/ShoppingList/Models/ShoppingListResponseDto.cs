using AutoMapper;
using WhatToBuy.Services.ShoppingLists;

namespace WhatToBuy.Api.Controllers.Models;

public class ShoppingListResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ItemResponseDto> Items { get; set; }
}

public class ShoppingListResponseDtoProfile : Profile
{
    public ShoppingListResponseDtoProfile()
    {
        CreateMap<ShoppingListModel, ShoppingListResponseDto>();
    }
}
