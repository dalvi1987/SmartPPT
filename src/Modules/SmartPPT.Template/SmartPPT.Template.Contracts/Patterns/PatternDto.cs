namespace SmartPPT.Template.Contracts.Patterns;

public sealed class PatternDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public PatternType PatternType { get; init; }

    public IReadOnlyCollection<PatternElementDto> Elements { get; init; } = Array.Empty<PatternElementDto>();
}
