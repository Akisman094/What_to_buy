using Microsoft.Extensions.DependencyInjection;
using WhatToBuy.Context.Repositories;

namespace WhatToBuy.Services.Items;

public static class Bootstrapper
{
    public static IServiceCollection AddItemService(this IServiceCollection services)
    {
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IItemRepository, ItemRepository>();

        return services;
    }
}
