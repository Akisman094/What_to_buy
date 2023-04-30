using AutoMapper;
using WhatToBuy.Services.Users;

namespace WhatToBuy.Api.Controllers.Models;

public class UserRegistrationRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
}

public class UserRegistrationRequestDtoProfile : Profile
{
    public UserRegistrationRequestDtoProfile()
    {
        CreateMap<UserRegistrationRequestDto, UserRegistrationModel>();
    }
}
