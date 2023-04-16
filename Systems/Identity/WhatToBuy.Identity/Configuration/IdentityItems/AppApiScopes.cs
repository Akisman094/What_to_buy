namespace WhatToBuy.Identity.Configuration;

using WhatToBuy.Common.Security;
using Duende.IdentityServer.Models;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.Admin, "Admin priviledges"),
        };
}