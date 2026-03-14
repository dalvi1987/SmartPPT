using SmartPPT.Storage.Contracts.DTOs;
using SmartPPT.Storage.Contracts.Responses;
using SmartPPT.Storage.Domain.Documents;

namespace SmartPPT.Storage.Application.Mappers;

public static class DocumentMapper
{
    public static DocumentDto ToDto(Document document)
    {
        return new DocumentDto
        {
            Id = document.Id.ToString(),
            PresentationId = document.PresentationId,
            Format = document.Format.ToString(),
            FilePath = document.FilePath,
            CreatedAt = document.CreatedAt
        };
    }

    public static GenerateDocumentResponse ToResponse(Document document)
    {
        return new GenerateDocumentResponse
        {
            DocumentId = document.Id.ToString(),
            FilePath = document.FilePath
        };
    }
}
