namespace SmartPPT.Storage.Domain.Files;

public sealed class StoredFile
{
    public DateTime CreatedAt { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;

    public long Size { get; set; }
}
