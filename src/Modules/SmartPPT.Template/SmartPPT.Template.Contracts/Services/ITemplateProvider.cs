using SmartPPT.Template.Contracts.Layouts;
using SmartPPT.Template.Contracts.Patterns;
using SmartPPT.Template.Contracts.Rules;
using SmartPPT.Template.Contracts.Templates;

namespace SmartPPT.Template.Contracts.Services;

public interface ITemplateProvider
{
    TemplateDto? GetTemplate(Guid templateId);

    IReadOnlyCollection<TemplateDto> GetTemplates();

    IReadOnlyCollection<PatternDto> GetPatterns(Guid templateId);

    LayoutGridDto? GetLayoutGrid(Guid templateId);

    StyleRulesDto? GetStyleRules(Guid templateId);
}
