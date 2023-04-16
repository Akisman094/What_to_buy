using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

namespace WhatToBuy.Identity.Configuration.HealthCheck;

public class SelfHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("WhatToBuy.Identity");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
    }
}
