using Microsoft.Extensions.DependencyInjection;
using SmartPPT.SlideEngine.Application.Engines;
using SmartPPT.SlideEngine.Application.Interfaces;
using SmartPPT.SlideEngine.Application.Services;
using SmartPPT.SlideEngine.Contracts.Services;

namespace SmartPPT.SlideEngine.Application.DependencyInjection;

public static class SlideEngineApplicationModule
{
    public static IServiceCollection AddSlideEngineApplication(this IServiceCollection services)
    {
        services.AddScoped<ISlideLayoutEngine, SlideLayoutEngine>();
        services.AddScoped<ILayoutCalculator, GridLayoutCalculator>();
        services.AddScoped<PatternSlotResolver>();

        return services;
    }
}
