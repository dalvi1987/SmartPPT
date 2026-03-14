using LiteDB;
using SmartPPT.Template.Domain.Patterns;
using temp =SmartPPT.Template.Domain.Templates;
using SmartPPT.Template.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace SmartPPT.Template.Infrastructure.Persistence;

public sealed class LiteDbContext : IDisposable
{
    private readonly LiteDatabase _database;

    public LiteDbContext(IOptions<TemplateStorageOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        var dbPath = options.Value.DatabasePath;        
        if (string.IsNullOrWhiteSpace(dbPath))
            throw new ArgumentException("DatabasePath must be configured.");

        _database = new LiteDatabase(dbPath);
    }

    public ILiteCollection<temp.Template> Templates => _database.GetCollection<temp.Template>("templates");

    public ILiteCollection<Pattern> Patterns => _database.GetCollection<Pattern>("patterns");

    public void Dispose()
    {
        _database.Dispose();
    }
}
