using AutoMapper;
using FluentValidation;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Items;

public class ItemUpdateModel
{
    public string Name { get; set; }
    public int? Amount { get; set; }
}

public class ItemUpdateModelProfile : Profile
{
    public ItemUpdateModelProfile()
    {
        CreateMap<ItemUpdateModel, Item>();
    }
}

public class ItemUpdateModelValidator : AbstractValidator<ItemUpdateModel>
{
    public ItemUpdateModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Either Name or FamilyId should be present.").Unless(x => x.Amount is not null);
        RuleFor(x => x.Amount).NotEmpty().WithMessage("Either Name or FamilyId should be present.").Unless(x => x.Name != string.Empty);
    }
}
