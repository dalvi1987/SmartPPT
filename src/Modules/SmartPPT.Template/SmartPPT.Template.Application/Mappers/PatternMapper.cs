using SmartPPT.Template.Contracts.Patterns;
using DomainElementType = SmartPPT.Template.Domain.Enums.ElementType;
using DomainPatternType = SmartPPT.Template.Domain.Enums.PatternType;
using DomainPattern = SmartPPT.Template.Domain.Patterns.Pattern;
using DomainPatternElement = SmartPPT.Template.Domain.Patterns.PatternElement;

namespace SmartPPT.Template.Application.Mappers;

public static class PatternMapper
{
    public static PatternDto ToDto(DomainPattern pattern)
    {
        ArgumentNullException.ThrowIfNull(pattern);

        return new PatternDto
        {
            Id = pattern.Id,
            Name = pattern.Name,
            PatternType = MapPatternType(pattern.PatternType),
            Elements = pattern.Elements.Select(ToDto).ToArray()
        };
    }

    public static PatternElementDto ToDto(DomainPatternElement element)
    {
        ArgumentNullException.ThrowIfNull(element);

        return new PatternElementDto
        {
            ElementType = MapElementType(element.ElementType),
            SlotName = element.SlotName,
            MaxItems = element.MaxItems
        };
    }

    private static PatternType MapPatternType(DomainPatternType patternType) => patternType switch
    {
        DomainPatternType.Title => PatternType.Title,
        DomainPatternType.Chart => PatternType.Chart,
        DomainPatternType.Table => PatternType.Table,
        DomainPatternType.Insights => PatternType.Insights,
        DomainPatternType.Comparison => PatternType.Comparison,
        _ => throw new ArgumentOutOfRangeException(nameof(patternType), patternType, null)
    };

    private static ElementType MapElementType(DomainElementType elementType) => elementType switch
    {
        DomainElementType.Text => ElementType.Text,
        DomainElementType.Chart => ElementType.Chart,
        DomainElementType.Table => ElementType.Table,
        DomainElementType.Image => ElementType.Image,
        _ => throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null)
    };
}
