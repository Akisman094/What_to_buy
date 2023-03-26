using AutoMapper;
using WhatToBuy.Services.Families;
using WhatToBuy.Services.ShoppingLists;

namespace WhatToBuy.Api.Controllers.Family.Models;

public class FamilyResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<string> Users { get; set; }

    public ICollection<string> ShoppingLists { get; set; }
}

public class FamilyResponseDtoProfile : Profile
{
    public FamilyResponseDtoProfile()
    {
        CreateMap<FamilyModel, FamilyResponseDto>();
    }
}
