using AutoMapper;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Families;

public class FamilyModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<string>? UserNames { get; set; }

    public IEnumerable<int>? ShoppingLists { get; set; }
}

public class FamilyResponseDtoProfile : Profile
{
    public FamilyResponseDtoProfile()
    {
        CreateMap<Family, FamilyModel>().ForMember(dest => dest.UserNames, act => act.MapFrom(src => src.Users));
        CreateMap<User, string>().ConvertUsing(x => x.UserName);
        CreateMap<ShoppingList, int>().ConvertUsing(x => x.Id);
    }
}
