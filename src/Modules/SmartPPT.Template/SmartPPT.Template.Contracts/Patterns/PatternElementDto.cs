namespace SmartPPT.Template.Contracts.Patterns;

public sealed class PatternElementDto
{
    public ElementType ElementType { get; init; }

    public string SlotName { get; init; } = string.Empty;

    public int MaxItems { get; init; }
}
