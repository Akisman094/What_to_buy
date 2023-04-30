using AutoMapper;
using FluentValidation;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Users;
public class UserUpdateModel
{
    public string Name { get; set; } = string.Empty;
    public int? FamilyId { get; set; }
}

public class UserUpdateModelProfile : Profile
{
    public UserUpdateModelProfile()
    {
        CreateMap<UserUpdateModel, User>();
    }
}

public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
{
    public UserUpdateModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Either Name or FamilyId should be present.").Unless(x => x.FamilyId is not null);
        RuleFor(x => x.FamilyId).NotEmpty().WithMessage("Either Name or FamilyId should be present.").Unless(x => x.Name != string.Empty);
    }
}