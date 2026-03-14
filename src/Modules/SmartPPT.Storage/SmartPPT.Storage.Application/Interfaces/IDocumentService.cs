using SmartPPT.Storage.Contracts.DTOs;
using SmartPPT.Storage.Contracts.Requests;
using SmartPPT.Storage.Contracts.Responses;

namespace SmartPPT.Storage.Application.Interfaces;

public interface IDocumentService
{
    GenerateDocumentResponse DownloadDocument(string documentId);

    GenerateDocumentResponse GenerateDocument(GenerateDocumentRequest request);

    DocumentDto GetDocument(string documentId);
}
