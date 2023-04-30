namespace WhatToBuy.Api.Configuration;

using WhatToBuy.Common;

public static class ControllerConfiguration
{
    public static IServiceCollection AddAppController(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.SetDefaultSettings())
            .AddValidator()
            ;

        return services;
    }

    public static IEndpointRouteBuilder UseAppController(this IEndpointRouteBuilder app)
    {
        app.MapControllers();

        return app;
    }
}
