namespace WhatToBuy.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DbInitializer
{
    public static void Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);

        var dataBase = scope.ServiceProvider.GetRequiredService<MainDbContext>();
        dataBase.Database.Migrate();
    }
}
