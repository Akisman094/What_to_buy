using AutoMapper;
using WhatToBuy.Services.Users;

namespace WhatToBuy.Api.Controllers.Models;

public class UserUpdateRequestDto
{
    public string Name { get; set; }
    public int? FamilyId { get; set; }
}

public class UserUpdateRequestDtoProfile : Profile
{
    public UserUpdateRequestDtoProfile()
    {
        CreateMap<UserUpdateRequestDto, UserUpdateModel>();
    }

}
