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
        var settings = Settings.Load<DbSettings>("Database", configuration);
        services.AddSingleton(settings);

        services.AddDbContext<MainDbContext>(options =>
        {
            options.UseSqlServer(settings.ConnectionString);
        });

        return services;
    }
}