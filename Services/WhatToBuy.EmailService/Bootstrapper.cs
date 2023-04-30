namespace WhatToBuy.EmailService;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhatToBuy.Settings;

public static class Bootstrapper
{
    /// <summary>
    /// Register db context
    /// </summary>
    public static IServiceCollection AddAppEmailService(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.Load<EmailSenderSettings>("EmailSender", configuration);
        services.AddSingleton(settings);

        services.AddScoped<IEmailSenderService, EmailSenderService>();

        return services;
    }
}