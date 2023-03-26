using WhatToBuy.Api.Middlewares;

namespace WhatToBuy.Api.Configuration;

public static class MiddlewareConfiguration
{
    public static void UseAppMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionsMiddleware>();
    }
}
