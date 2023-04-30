using AutoMapper;
using FluentValidation;
using WhatToBuy.Services.Items;

namespace WhatToBuy.Api.Controllers.Models;

public class ItemAddRequestDto
{
    public string Name { get; set; }
    public int Amount { get; set; }
    public int ShoppingListId { get; set; }
}

public class ItemAddRequestDtoValidator : AbstractValidator<ItemAddRequestDto>
{
    public ItemAddRequestDtoValidator() 
    { 
        RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount can't be empty")
            .GreaterThan(0).WithMessage("Amount should be greater than 0");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(50).WithMessage("Name is too long");
        RuleFor(x => x.ShoppingListId).NotEmpty().WithMessage("ShoppingListId cannot be empty");
    }
}

public class ItemAddRequestDtoProfile : Profile
{
    public ItemAddRequestDtoProfile()
    {
        CreateMap<ItemAddRequestDto, ItemAddModel>();
    }
}