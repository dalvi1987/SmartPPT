using LiteDB;
using prsnt = SmartPPT.Presentation.Domain.Presentations;
using SmartPPT.Presentation.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace SmartPPT.Presentation.Infrastructure.Persistence;

public sealed class LiteDbContext : IDisposable
{
    private readonly LiteDatabase _database;

    public LiteDbContext(IOptions<PresentationStorageOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        var dbPath = options.Value.DatabasePath;        
        if (string.IsNullOrWhiteSpace(dbPath))
            throw new ArgumentException("DatabasePath must be configured.");

        _database = new LiteDatabase(dbPath);
    }

    public ILiteCollection<prsnt.Presentation> Presentations => _database.GetCollection<prsnt.Presentation>("presentations");

    public void Dispose()
    {
        _database.Dispose();
    }
}
