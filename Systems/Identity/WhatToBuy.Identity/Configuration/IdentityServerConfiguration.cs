using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using WhatToBuy.Context;
using WhatToBuy.Context.Entities;

namespace WhatToBuy.Identity.Configuration;

public static class IdentityServerConfiguration
{
    public static IServiceCollection AddAppIdentityServer(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>(opt =>
        {
            opt.Password.RequiredLength = 6;
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireNonAlphanumeric = false;
        })
            .AddEntityFrameworkStores<MainDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

        services
           .AddIdentityServer()

           .AddAspNetIdentity<User>()

           .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
           .AddInMemoryClients(AppClients.Clients)
           .AddInMemoryApiResources(AppResources.Resources)
           .AddInMemoryIdentityResources(AppIdentityResources.Resources)

           .AddTestUsers(AppApiTestUsers.ApiUsers)

           .AddDeveloperSigningCredential();
        return services;
    }

}
