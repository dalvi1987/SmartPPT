using System.Text.Json;
using SmartPPT.Agent.Infrastructure.Configuration;
using SmartPPT.Shared.BuildingBlocks.AI;

namespace SmartPPT.Agent.Infrastructure.LLM;

public sealed class OpenAiClient : ILLMClient
{
    private readonly AgentOptions _options;

    public OpenAiClient(AgentOptions options)
    {
        _options = options;
    }

    public Task<string> GenerateAsync(string prompt, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var model = string.IsNullOrWhiteSpace(_options.Model) ? "unspecified-model" : _options.Model;
        return Task.FromResult($"[Simulated {model} response]{Environment.NewLine}{prompt}");
    }

    public Task<string> GenerateTextAsync(string prompt, CancellationToken cancellationToken = default)
    {
        return GenerateAsync(prompt, cancellationToken);
    }

    public Task<TResponse?> GenerateStructuredAsync<TResponse>(
        string prompt,
        JsonSerializerOptions? serializerOptions = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(default(TResponse));
    }
}
