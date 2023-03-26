using AutoMapper;
using FluentValidation;
using WhatToBuy.Services.ShoppingLists;

namespace WhatToBuy.Api.Controllers.Models;

public class ShoppingListUpdateRequestDto
{
    public string Name { get; set; }
}

public class ShoppingListUpdateRequestDtoValidator : AbstractValidator<ShoppingListUpdateRequestDto>
{
    public ShoppingListUpdateRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(50).WithMessage("Name is too long");
    }
}

public class ShoppingListUpdateRequestDtoProfile : Profile
{
    public ShoppingListUpdateRequestDtoProfile()
    {
        CreateMap<ShoppingListUpdateRequestDto, ShoppingListUpdateModel>();
    }
}