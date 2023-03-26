using AutoMapper;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Services.Families;

public class FamilyModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<string> Users { get; set; }

    public ICollection<string> ShoppingLists { get; set; }
}

public class FamilyResponseDtoProfile : Profile
{
    public FamilyResponseDtoProfile()
    {
        CreateMap<Family, FamilyModel>()
            .AfterMap((s, d) =>
            {
                d.Users = new List<string>(s.Users.Select(x => x.Name));
                d.ShoppingLists = new List<string>(s.ShoppingLists.Select(x => x.Name));
            });
    }
}
