using Microsoft.Extensions.DependencyInjection;
using SmartPPT.Presentation.Application.Interfaces;
using SmartPPT.Presentation.Application.Orchestrators;
using SmartPPT.Presentation.Application.Repositories;
using SmartPPT.Presentation.Application.Services;
using SmartPPT.Presentation.Contracts.Services;
using SmartPPT.Presentation.Infrastructure.Configuration;
using SmartPPT.Presentation.Infrastructure.Persistence;
using SmartPPT.Presentation.Infrastructure.Repositories;

namespace SmartPPT.Presentation.Infrastructure.DependencyInjection;

public static class PresentationModule
{
    public static IServiceCollection AddPresentationModule(this IServiceCollection services)
    {
        services.AddSingleton(new PresentationStorageOptions());
        services.AddSingleton<LiteDbContext>();
        services.AddScoped<IPresentationRepository, PresentationRepository>();
        services.AddScoped<IPresentationOrchestrator, PresentationOrchestrator>();
        services.AddScoped<IPresentationService, PresentationService>();

        return services;
    }
}
