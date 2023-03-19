namespace WhatToBuy.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhatToBuy.Settings;

public static class Bootstrapper
{
    /// <summary>
    /// Register db context
    /// </summary>
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.LoadAll();
        services.AddSingleton(settings);

        services.AddDbContext<MainDbContext>(options =>
        {
            options.UseSqlServer(settings.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}