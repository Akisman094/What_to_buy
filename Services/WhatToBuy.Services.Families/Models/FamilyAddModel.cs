using AutoMapper;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Families;

public class FamilyAddModel
{
    public string Name { get; set; }
}

public class FamilyAddModelProfile : Profile
{
	public FamilyAddModelProfile()
	{
		CreateMap<FamilyAddModel, Family>();
	}
}