using AutoMapper;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Users;

public class UserModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public int FamilyId { get; set; }
}

public class UserModelProfile : Profile 
{
    public UserModelProfile()
    {
        CreateMap<User, UserModel>();
    }
}