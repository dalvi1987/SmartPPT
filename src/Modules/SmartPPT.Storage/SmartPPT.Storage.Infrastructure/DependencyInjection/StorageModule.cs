using Microsoft.Extensions.DependencyInjection;
using SmartPPT.Storage.Application.Builders;
using SmartPPT.Storage.Application.Interfaces;
using SmartPPT.Storage.Application.Repositories;
using SmartPPT.Storage.Application.Services;
using SmartPPT.Storage.Contracts.Services;
using SmartPPT.Storage.Infrastructure.Builders;
using SmartPPT.Storage.Infrastructure.Configuration;
using SmartPPT.Storage.Infrastructure.Persistence;
using SmartPPT.Storage.Infrastructure.Repositories;

namespace SmartPPT.Storage.Infrastructure.DependencyInjection;

public static class StorageModule
{
    public static IServiceCollection AddStorageModule(this IServiceCollection services)
    {
        services.AddSingleton(new StorageOptions());
        services.AddSingleton<LiteDbContext>();
        services.AddScoped<IDocumentBuilder, PptDocumentBuilder>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IDocumentGenerator, DocumentGenerator>();
        services.AddScoped<IDocumentService, DocumentGenerator>();

        return services;
    }
}
