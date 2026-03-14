using SmartPPT.SlideEngine.Contracts.Layout;
using SmartPPT.Storage.Application.Builders;
using SmartPPT.Storage.Application.Interfaces;
using SmartPPT.Storage.Application.Mappers;
using SmartPPT.Storage.Application.Repositories;
using SmartPPT.Storage.Contracts.DTOs;
using SmartPPT.Storage.Contracts.Requests;
using SmartPPT.Storage.Contracts.Responses;
using SmartPPT.Storage.Contracts.Services;
using SmartPPT.Storage.Domain.Documents;
using SmartPPT.Storage.Domain.Enums;

namespace SmartPPT.Storage.Application.Services;

public sealed class DocumentGenerator : IDocumentGenerator, IDocumentService
{
    private readonly IDocumentBuilder _documentBuilder;
    private readonly IDocumentRepository _documentRepository;

    public DocumentGenerator(
        IDocumentBuilder documentBuilder,
        IDocumentRepository documentRepository)
    {
        _documentBuilder = documentBuilder;
        _documentRepository = documentRepository;
    }

    public GenerateDocumentResponse DownloadDocument(string documentId)
    {
        if (!Guid.TryParse(documentId, out var parsedDocumentId))
        {
            return new GenerateDocumentResponse();
        }

        var document = _documentRepository.GetDocument(parsedDocumentId);
        return document is null ? new GenerateDocumentResponse() : DocumentMapper.ToResponse(document);
    }

    public GenerateDocumentResponse GenerateDocument(GenerateDocumentRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var generatedFiles = request.Slides
            .Select(slide => _documentBuilder.BuildDocument(new RenderableSlideDto
            {
                Title = slide,
                PositionedElements = []
            }))
            .ToList();

        var primaryFile = generatedFiles.LastOrDefault();

        var document = new Document
        {
            Id = Guid.NewGuid(),
            PresentationId = request.PresentationId,
            Format = MapFormat(request.Format),
            FilePath = primaryFile?.Path ?? string.Empty,
            CreatedAt = DateTime.UtcNow
        };

        _documentRepository.SaveDocument(document);

        return DocumentMapper.ToResponse(document);
    }

    public DocumentDto GetDocument(string documentId)
    {
        if (!Guid.TryParse(documentId, out var parsedDocumentId))
        {
            return new DocumentDto();
        }

        var document = _documentRepository.GetDocument(parsedDocumentId);
        return document is null ? new DocumentDto() : DocumentMapper.ToDto(document);
    }

    private static ExportFormat MapFormat(string format)
    {
        return Enum.TryParse<ExportFormat>(format, true, out var exportFormat)
            ? exportFormat
            : ExportFormat.Pptx;
    }
}
