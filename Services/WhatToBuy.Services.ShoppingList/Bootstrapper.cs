using Microsoft.Extensions.DependencyInjection;

namespace WhatToBuy.Services.ShoppingLists;

public static class Bootstrapper
{
    public static IServiceCollection AddShoppingListService(this IServiceCollection services)
    {
        services.AddSingleton<IShoppingListService, ShoppingListService>();

        return services;
    }
}
