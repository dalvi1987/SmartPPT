using Microsoft.Extensions.Options;
using SmartPPT.SlideEngine.Contracts.Layout;
using SmartPPT.Storage.Application.Builders;
using SmartPPT.Storage.Domain.Files;
using SmartPPT.Storage.Infrastructure.Configuration;

namespace SmartPPT.Storage.Infrastructure.Builders;

public sealed class PptDocumentBuilder : IDocumentBuilder
{
    private readonly StorageOptions _options;

    public PptDocumentBuilder(IOptions<StorageOptions> options)
    {
        _options = options.Value;
    }

    public StoredFile BuildDocument(RenderableSlideDto slide)
    {
        ArgumentNullException.ThrowIfNull(slide);

        var safeTitle = string.IsNullOrWhiteSpace(slide.Title) ? "slide" : slide.Title;
        var fileName = $"{safeTitle}-{Guid.NewGuid():N}.pptx";

        return new StoredFile
        {
            FileName = fileName,
            Path = System.IO.Path.Combine(_options.StoragePath, fileName),
            Size = 0,
            CreatedAt = DateTime.UtcNow
        };
    }
}
