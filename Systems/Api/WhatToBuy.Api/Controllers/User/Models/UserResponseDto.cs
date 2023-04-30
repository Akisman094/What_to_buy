using AutoMapper;
using WhatToBuy.Services.Users;

namespace WhatToBuy.Api.Controllers.Models;

internal class UserResponseDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public int FamilyId { get; set; }
}

public class UserModelProfile : Profile
{
    public UserModelProfile()
    {
        CreateMap<UserModel, UserResponseDto>();
    }
}