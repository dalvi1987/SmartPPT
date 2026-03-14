namespace SmartPPT.Storage.Infrastructure.Configuration;

public sealed class StorageOptions
{
    public string DatabasePath { get; set; } = "smartppt-storage.db";

    public string StoragePath { get; set; } = "storage";
}
