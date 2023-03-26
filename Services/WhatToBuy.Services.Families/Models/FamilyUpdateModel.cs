using AutoMapper;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Families;

public class FamilyUpdateModel
{
    public string Name { get; set; }
}

public class FamilyUpdateModelProfile : Profile
{
    public FamilyUpdateModelProfile()
    {
        CreateMap<FamilyUpdateModel, Family>();
    }
}