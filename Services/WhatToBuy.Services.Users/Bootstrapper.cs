using Microsoft.Extensions.DependencyInjection;
using WhatToBuy.Context.Repositories;

namespace WhatToBuy.Services.Users;

public static class Bootstrapper
{
    public static IServiceCollection AddUsersService(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();

        return services;
    }
}
