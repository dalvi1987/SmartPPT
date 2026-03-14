using Microsoft.Extensions.DependencyInjection;
using SmartPPT.Template.Application.Interfaces;
using SmartPPT.Template.Application.Services;
using SmartPPT.Template.Contracts.Services;
using SmartPPT.Template.Infrastructure.Configuration;
using SmartPPT.Template.Infrastructure.Persistence;
using SmartPPT.Template.Infrastructure.Repositories;

namespace SmartPPT.Template.Infrastructure.DependencyInjection;

public static class TemplateModule
{
    public static IServiceCollection AddTemplateModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton(new TemplateStorageOptions());
        services.AddSingleton<LiteDbContext>();
        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<TemplateRepository>();
        services.AddScoped<ITemplateProvider, TemplateProvider>();
        services.AddScoped<TemplateProvider>();

        return services;
    }
}
