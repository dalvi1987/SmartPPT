using LiteDB;
using Microsoft.Extensions.Options;
using SmartPPT.Storage.Domain.Documents;
using SmartPPT.Storage.Infrastructure.Configuration;

namespace SmartPPT.Storage.Infrastructure.Persistence;

public sealed class LiteDbContext : IDisposable
{
    private readonly LiteDatabase _database;

    public LiteDbContext(IOptions<StorageOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        var dbPath = options.Value.DatabasePath;        
        if (string.IsNullOrWhiteSpace(dbPath))
            throw new ArgumentException("DatabasePath must be configured.");

        _database = new LiteDatabase(dbPath);
    }

    public ILiteCollection<Document> Documents => _database.GetCollection<Document>("documents");

    public void Dispose()
    {
        _database.Dispose();
    }
}
