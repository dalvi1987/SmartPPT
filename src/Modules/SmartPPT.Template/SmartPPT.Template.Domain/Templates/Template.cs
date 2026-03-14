using SmartPPT.Shared.SharedKernel.Abstractions;
using SmartPPT.Shared.SharedKernel.Entities;
using SmartPPT.Template.Domain.Layouts;
using SmartPPT.Template.Domain.Patterns;
using SmartPPT.Template.Domain.Rules;
using SmartPPT.Template.Domain.Themes;

namespace SmartPPT.Template.Domain.Templates;

public sealed class Template : BaseEntity, IAggregateRoot
{
    private readonly List<Pattern> _patterns = [];

    public Template(
        Guid id,
        string name,
        Theme theme,
        LayoutGrid layoutGrid,
        StyleRules styleRules,
        IEnumerable<Pattern>? patterns = null)
        : base(id)
    {
        Name = name;
        Theme = theme;
        LayoutGrid = layoutGrid;
        StyleRules = styleRules;

        if (patterns is not null)
        {
            _patterns.AddRange(patterns);
        }
    }

    public string Name { get; private set; }

    public Theme Theme { get; private set; }

    public LayoutGrid LayoutGrid { get; private set; }

    public StyleRules StyleRules { get; private set; }

    public IReadOnlyCollection<Pattern> Patterns => _patterns.AsReadOnly();
}
