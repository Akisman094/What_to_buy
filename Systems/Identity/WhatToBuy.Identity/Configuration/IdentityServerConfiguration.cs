using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using WhatToBuy.Context;
using WhatToBuy.Context.Entities;
using Duende.IdentityServer.AspNetIdentity;

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
           .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
           .AddInMemoryClients(AppClients.Clients)
           .AddInMemoryApiResources(AppResources.Resources)
           .AddInMemoryIdentityResources(AppIdentityResources.Resources)
           .AddResourceOwnerValidator<ResourceOwnerPasswordValidator<User>>()
           .AddAspNetIdentity<User>()
           .AddProfileService<ProfileService>()
           .AddDeveloperSigningCredential();

        services.AddScoped<IProfileService, ProfileService>();
        return services;
    }
}
