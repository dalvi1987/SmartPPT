using SmartPPT.Template.Infrastructure.Configuration;

namespace SmartPPT.Template.Infrastructure.Loaders;

public sealed class TemplateLoader
{
    private static readonly string[] SupportedExtensions = [".pptx", ".ppt"];
    private readonly TemplateStorageOptions _options;

    public TemplateLoader(TemplateStorageOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public byte[] LoadTemplate(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path must be provided.", nameof(filePath));
        }

        return File.ReadAllBytes(filePath);
    }

    public IReadOnlyCollection<byte[]> LoadDefaultTemplates()
    {
        if (string.IsNullOrWhiteSpace(_options.TemplateFolder) || !Directory.Exists(_options.TemplateFolder))
        {
            return Array.Empty<byte[]>();
        }

        return Directory
            .EnumerateFiles(_options.TemplateFolder)
            .Where(file => SupportedExtensions.Contains(Path.GetExtension(file), StringComparer.OrdinalIgnoreCase))
            .Select(LoadTemplate)
            .ToArray();
    }
}
