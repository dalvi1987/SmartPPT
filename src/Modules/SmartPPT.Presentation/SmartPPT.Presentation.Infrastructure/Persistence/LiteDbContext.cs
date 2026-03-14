using LiteDB;
using prsnt = SmartPPT.Presentation.Domain.Presentations;
using SmartPPT.Presentation.Infrastructure.Configuration;

namespace SmartPPT.Presentation.Infrastructure.Persistence;

public sealed class LiteDbContext : IDisposable
{
    private readonly LiteDatabase _database;

    public LiteDbContext(PresentationStorageOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (string.IsNullOrWhiteSpace(options.DatabasePath))
        {
            throw new ArgumentException("DatabasePath must be configured.", nameof(options));
        }

        _database = new LiteDatabase(options.DatabasePath);
    }

    public ILiteCollection<prsnt.Presentation> Presentations => _database.GetCollection<prsnt.Presentation>("presentations");

    public void Dispose()
    {
        _database.Dispose();
    }
}
