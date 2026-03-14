using System.Text.Json;

namespace SmartPPT.Shared.BuildingBlocks.AI;

public interface ILLMClient
{
    Task<string> GenerateTextAsync(string prompt, CancellationToken cancellationToken = default);

    Task<TResponse?> GenerateStructuredAsync<TResponse>(
        string prompt,
        JsonSerializerOptions? serializerOptions = null,
        CancellationToken cancellationToken = default);
}
