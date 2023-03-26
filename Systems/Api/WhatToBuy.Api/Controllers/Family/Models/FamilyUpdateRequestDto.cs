using AutoMapper;
using FluentValidation;
using WhatToBuy.Services.Families;

namespace WhatToBuy.Api.Controllers.Family.Models;

public class FamilyUpdateRequestDto
{
    public string Name { get; set; }
}
public class FamilyUpdateRequestDtoValidator : AbstractValidator<FamilyUpdateRequestDto>
{
    public FamilyUpdateRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters");
    }
}

public class FamilyUpdateRequestDtoProfile : Profile
{
    public FamilyUpdateRequestDtoProfile()
    {
        CreateMap<FamilyUpdateRequestDto, FamilyUpdateModel>();
    }
}