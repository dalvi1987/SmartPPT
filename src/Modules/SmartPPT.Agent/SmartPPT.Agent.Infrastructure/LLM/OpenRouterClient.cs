using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using SmartPPT.Agent.Infrastructure.Configuration;
using SmartPPT.Shared.BuildingBlocks.AI;

namespace SmartPPT.Agent.Infrastructure.LLM;

public sealed class OpenRouterClient : ILLMClient
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<AgentOptions> _options;

    public OpenRouterClient(HttpClient httpClient, IOptions<AgentOptions> options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public async Task<string> GenerateAsync(string prompt, CancellationToken cancellationToken = default)
    {
        var settings = _options.Value.OpenRouter;
        _httpClient.BaseAddress = new Uri(settings.BaseUrl);

        using var response = await _httpClient.PostAsJsonAsync("chat/completions", new
        {
            model = settings.Model,
            temperature = settings.Temperature,
            max_tokens = settings.MaxTokens,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        }, cancellationToken);

        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        return json.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? string.Empty;
    }

    public Task<string> GenerateTextAsync(string prompt, CancellationToken cancellationToken = default)
    {
        return GenerateAsync(prompt, cancellationToken);
    }

    public async Task<TResponse?> GenerateStructuredAsync<TResponse>(
        string prompt,
        JsonSerializerOptions? serializerOptions = null,
        CancellationToken cancellationToken = default)
    {
        var text = await GenerateAsync(prompt, cancellationToken);
        return JsonSerializer.Deserialize<TResponse>(text, serializerOptions);
    }
}
