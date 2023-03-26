using WhatToBuy.Services.Items;
using WhatToBuy.Services.ShoppingLists;

namespace WhatToBuy.Api;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddItemService()
            .AddShoppingListService();

        return services;
    }
}
