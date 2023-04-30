using WhatToBuy.Services.Items;
using WhatToBuy.Services.ShoppingLists;
using WhatToBuy.Services.Families;
using WhatToBuy.Services.Users;
using WhatToBuy.EmailService;

namespace WhatToBuy.Api;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddItemService()
            .AddShoppingListService()
            .AddFamilyService()
            .AddUsersService()
            .AddAppEmailService();

        return services;
    }
}
