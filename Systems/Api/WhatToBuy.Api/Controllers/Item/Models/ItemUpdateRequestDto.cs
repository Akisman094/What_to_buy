using AutoMapper;
using FluentValidation;
using WhatToBuy.Api.Controllers.Models;
using WhatToBuy.Services.Items;

namespace WhatToBuy.Api.Controllers.Item.Models;

public class ItemUpdateRequestDto
{
    public string Name { get; set; }
    public int Amount { get; set; }
}

public class ItemUpdateRequestDtoValidator : AbstractValidator<ItemUpdateRequestDto>
{
    public ItemUpdateRequestDtoValidator()
    {
        RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount can't be empty")
            .GreaterThan(0).WithMessage("Amount should be greater than 0");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(50).WithMessage("Name is too long");
    }
}

public class ItemUpdateRequestDtoProfile : Profile
{
    public ItemUpdateRequestDtoProfile()
    {
        CreateMap<ItemUpdateRequestDto, ItemUpdateModel>();
    }
}