using SmartPPT.Shared.SharedKernel.Entities;
using SmartPPT.Template.Domain.Enums;

namespace SmartPPT.Template.Domain.Patterns;

public sealed class Pattern : BaseEntity
{
    private readonly List<PatternElement> _elements = [];

    public Pattern(Guid id, string name, PatternType patternType, IEnumerable<PatternElement>? elements = null)
        : base(id)
    {
        Name = name;
        PatternType = patternType;

        if (elements is not null)
        {
            _elements.AddRange(elements);
        }
    }

    public string Name { get; private set; }

    public PatternType PatternType { get; private set; }

    public IReadOnlyCollection<PatternElement> Elements => _elements.AsReadOnly();
}
