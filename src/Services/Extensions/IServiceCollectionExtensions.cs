using Abstractions.Services.Payment;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

using Services.Email;
using Services.Payment;

namespace Services.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddStripeServices(this IServiceCollection services)
    {
        services.AddScoped<ElasticEmailCredentials>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IStripeRefundService, StripeRefundService>();
        services.AddScoped<IStripeSessionService, StripeSessionService>();

        return services;
    }
}
