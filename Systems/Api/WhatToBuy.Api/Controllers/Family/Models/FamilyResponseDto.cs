using AutoMapper;
using WhatToBuy.Services.Families;

namespace WhatToBuy.Api.Controllers.Family.Models;

public class FamilyResponseDto
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
        CreateMap<FamilyModel, FamilyResponseDto>();
    }
}
