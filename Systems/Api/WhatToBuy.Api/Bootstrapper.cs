using WhatToBuy.Services.Items;
using WhatToBuy.Services.ShoppingLists;
using WhatToBuy.Services.Families;

namespace WhatToBuy.Api;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddItemService()
            .AddShoppingListService()
            .AddFamilyService();

        return services;
    }
}
