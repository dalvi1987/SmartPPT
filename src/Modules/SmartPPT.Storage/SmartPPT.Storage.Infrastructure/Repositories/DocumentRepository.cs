using LiteDB;
using SmartPPT.Storage.Application.Repositories;
using SmartPPT.Storage.Domain.Documents;
using SmartPPT.Storage.Infrastructure.Persistence;

namespace SmartPPT.Storage.Infrastructure.Repositories;

public sealed class DocumentRepository : IDocumentRepository
{
    private readonly LiteDbContext _context;

    public DocumentRepository(LiteDbContext context)
    {
        _context = context;
    }

    public Document? GetDocument(Guid documentId)
    {
        return _context.Documents.FindById(new BsonValue(documentId));
    }

    public IReadOnlyCollection<Document> ListDocuments()
    {
        return _context.Documents.FindAll().ToList();
    }

    public void SaveDocument(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);

        _context.Documents.Upsert(document);
    }
}
