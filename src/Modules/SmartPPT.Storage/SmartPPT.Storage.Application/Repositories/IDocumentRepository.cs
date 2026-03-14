using SmartPPT.Storage.Domain.Documents;

namespace SmartPPT.Storage.Application.Repositories;

public interface IDocumentRepository
{
    Document? GetDocument(Guid documentId);

    IReadOnlyCollection<Document> ListDocuments();

    void SaveDocument(Document document);
}
