using AutoMapper;
using FluentValidation;
using WhatToBuy.Services.Families;

namespace WhatToBuy.Api.Controllers.Family.Models;

public class FamilyAddRequestDto
{
    public string Name { get; set; }
}

public class FamilyAddRequestDtoValidator : AbstractValidator<FamilyAddRequestDto>
{
	public FamilyAddRequestDtoValidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
			.MaximumLength(50).WithMessage("Name cannot be longer than 50 characters");
	}
}

public class FamilyAddRequestDtoProfile : Profile
{
	public FamilyAddRequestDtoProfile()
	{
		CreateMap<FamilyAddRequestDto, FamilyAddModel>();
	}
}