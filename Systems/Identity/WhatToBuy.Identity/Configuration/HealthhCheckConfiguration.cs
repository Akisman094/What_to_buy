using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WhatToBuy.Common;
using WhatToBuy.Identity.Configuration.HealthCheck;

namespace WhatToBuy.Identity.Configuration;

public static class HealthhCheckConfiguration
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<SelfHealthCheck>("DSRNetSchool.Identity");

        return services;
    }

    public static void UseAppHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");

        app.MapHealthChecks("/health/detail", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckHelper.WriteHealthCheckResponse,
            AllowCachingResponses = false,
        });
    }
}
