using AutoMapper;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.ShoppingLists;

public class ShoppingListUpdateModel
{ 
    public int Name { get; set; }
}

public class ShoppingListUpdateModelProfile : Profile
{
	public ShoppingListUpdateModelProfile()
	{
		CreateMap<ShoppingListUpdateModel, ShoppingList>();
	}
}
