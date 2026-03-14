using LiteDB;
using SmartPPT.Template.Domain.Patterns;
using temp =SmartPPT.Template.Domain.Templates;
using SmartPPT.Template.Infrastructure.Configuration;

namespace SmartPPT.Template.Infrastructure.Persistence;

public sealed class LiteDbContext : IDisposable
{
    private readonly LiteDatabase _database;

    public LiteDbContext(TemplateStorageOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (string.IsNullOrWhiteSpace(options.DatabasePath))
        {
            throw new ArgumentException("DatabasePath must be configured.", nameof(options));
        }

        _database = new LiteDatabase(options.DatabasePath);
    }

    public ILiteCollection<temp.Template> Templates => _database.GetCollection<temp.Template>("templates");

    public ILiteCollection<Pattern> Patterns => _database.GetCollection<Pattern>("patterns");

    public void Dispose()
    {
        _database.Dispose();
    }
}
