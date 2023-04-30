using AutoMapper;
using FluentValidation;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Users;

public class UserRegistrationModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; } 
}

public class UserRegistrationModelProfile : Profile
{
    public UserRegistrationModelProfile()
    {
        CreateMap<UserRegistrationModel, User>()
            .ForSourceMember(src => src.Password, opt => opt.DoNotValidate())
            .ForMember(dest => dest.Status, act => act.MapFrom(src => UserStatus.Active))
            .ForMember(dest => dest.EmailConfirmed, act => act.MapFrom(src => true))
            .ForMember(dest => dest.PhoneNumberConfirmed, act => act.MapFrom(src => false));
    }
}

public class UserRegistrationModelValidator : AbstractValidator<UserRegistrationModel>
{
    public UserRegistrationModelValidator()
    {
        RuleFor(x => x.Name).MaximumLength(50).WithMessage("Name is too long.")
            .NotEmpty().WithMessage("Name can't be empty.");
        RuleFor(x => x.UserName).MaximumLength(50).WithMessage("UserName is too long.")
            .NotEmpty().WithMessage("UserName can't be empty.");
        RuleFor(x => x.Email).MaximumLength(50).WithMessage("Email is too long.")
            .NotEmpty().WithMessage("Email can't be empty.");
        RuleFor(x => x.Password).MaximumLength(50).WithMessage("Password is too long.")
            .NotEmpty().WithMessage("Password can't be empty.");
    }
}