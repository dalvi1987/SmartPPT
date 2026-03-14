using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using SmartPPT.Agent.Infrastructure.Configuration;
using SmartPPT.Shared.BuildingBlocks.AI;

namespace SmartPPT.Agent.Infrastructure.LLM;

public sealed class OllamaClient : ILLMClient
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<AgentOptions> _options;

    public OllamaClient(HttpClient httpClient, IOptions<AgentOptions> options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public async Task<string> GenerateAsync(string prompt, CancellationToken cancellationToken = default)
    {
        var settings = _options.Value.Ollama;
        _httpClient.BaseAddress = new Uri(settings.BaseUrl);

        using var response = await _httpClient.PostAsJsonAsync("api/generate", new
        {
            model = settings.Model,
            prompt,
            stream = false,
            options = new
            {
                temperature = settings.Temperature,
                num_predict = settings.MaxTokens
            }
        }, cancellationToken);

        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var json = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        return json.RootElement.GetProperty("response").GetString() ?? string.Empty;
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
