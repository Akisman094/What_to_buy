using Microsoft.Extensions.DependencyInjection;

namespace WhatToBuy.Services.Items;

public static class Bootstrapper
{
    public static IServiceCollection AddItemService(this IServiceCollection services)
    {
        services.AddSingleton<IItemService, ItemService>();

        return services;
    }
}
