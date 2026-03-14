using Microsoft.Extensions.DependencyInjection;
using SmartPPT.SlideEngine.Application.Engines;
using SmartPPT.SlideEngine.Application.Interfaces;
using SmartPPT.SlideEngine.Application.Services;
using SmartPPT.SlideEngine.Contracts.Services;
using SmartPPT.SlideEngine.Infrastructure.Calculators;
using SmartPPT.SlideEngine.Infrastructure.Layout;

namespace SmartPPT.SlideEngine.Infrastructure.DependencyInjection;

public static class SlideEngineModule
{
    public static IServiceCollection AddSlideEngineModule(this IServiceCollection services)
    {
        services.AddScoped<ISlideLayoutEngine, SlideLayoutEngine>();
        services.AddScoped<ILayoutCalculator, LayoutCalculator>();
        services.AddScoped<PatternSlotResolver>();
        services.AddScoped<GridLayoutEngine>();

        return services;
    }
}
