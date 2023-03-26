using AutoMapper;
using FluentValidation;
using WhatToBuy.Services.ShoppingLists;

namespace WhatToBuy.Api.Controllers.Models;

public class AddShoppingListRequestDto
{
    public string Name { get; set; }
}

public class AddShoppingListRequestDtoValidator : AbstractValidator<AddShoppingListRequestDto>
{
    public AddShoppingListRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(50).WithMessage("Name is too long");
    }
}

public class AddShoppingListRequestDtoProfile : Profile
{
    public AddShoppingListRequestDtoProfile()
    {
        CreateMap<AddShoppingListModel, AddShoppingListRequestDto>();
    }
}