using Microsoft.Extensions.DependencyInjection;
using WhatToBuy.Context.Repositories;

namespace WhatToBuy.Services.Families;
public static class Bootstrapper
{
    public static IServiceCollection AddFamilyService(this IServiceCollection services)
    {
        services.AddScoped<IFamilyServices, FamilyServices>();
        services.AddScoped<IFamilyRepository, FamilyRepository>();

        return services;
    }
}
