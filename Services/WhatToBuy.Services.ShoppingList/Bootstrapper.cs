using Microsoft.Extensions.DependencyInjection;
using WhatToBuy.Context.Repositories;

namespace WhatToBuy.Services.ShoppingLists;

public static class Bootstrapper
{
    public static IServiceCollection AddShoppingListService(this IServiceCollection services)
    {
        services.AddScoped<IShoppingListService, ShoppingListService>();
        services.AddScoped<IShoppingListRepository, ShoppingListRepository>();

        return services;
    }
}
