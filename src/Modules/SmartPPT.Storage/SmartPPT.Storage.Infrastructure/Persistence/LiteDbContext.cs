using LiteDB;
using SmartPPT.Storage.Domain.Documents;
using SmartPPT.Storage.Infrastructure.Configuration;

namespace SmartPPT.Storage.Infrastructure.Persistence;

public sealed class LiteDbContext : IDisposable
{
    private readonly LiteDatabase _database;

    public LiteDbContext(StorageOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (string.IsNullOrWhiteSpace(options.DatabasePath))
        {
            throw new ArgumentException("DatabasePath must be configured.", nameof(options));
        }

        _database = new LiteDatabase(options.DatabasePath);
    }

    public ILiteCollection<Document> Documents => _database.GetCollection<Document>("documents");

    public void Dispose()
    {
        _database.Dispose();
    }
}
