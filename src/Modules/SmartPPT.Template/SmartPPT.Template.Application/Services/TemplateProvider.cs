using SmartPPT.Template.Application.Interfaces;
using SmartPPT.Template.Application.Mappers;
using SmartPPT.Template.Contracts.Layouts;
using SmartPPT.Template.Contracts.Patterns;
using SmartPPT.Template.Contracts.Rules;
using SmartPPT.Template.Contracts.Services;
using SmartPPT.Template.Contracts.Templates;

namespace SmartPPT.Template.Application.Services;

public sealed class TemplateProvider : ITemplateProvider
{
    private readonly ITemplateRepository _templateRepository;

    public TemplateProvider(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public TemplateDto? GetTemplate(Guid templateId)
    {
        var template = _templateRepository.GetTemplateById(templateId);

        return template is null ? null : TemplateMapper.ToDto(template);
    }

    public IReadOnlyCollection<TemplateDto> GetTemplates()
    {
        return _templateRepository
            .GetAllTemplates()
            .Select(TemplateMapper.ToDto)
            .ToArray();
    }

    public IReadOnlyCollection<PatternDto> GetPatterns(Guid templateId)
    {
        var template = _templateRepository.GetTemplateById(templateId);

        return template is null
            ? Array.Empty<PatternDto>()
            : template.Patterns.Select(PatternMapper.ToDto).ToArray();
    }

    public LayoutGridDto? GetLayoutGrid(Guid templateId)
    {
        var template = _templateRepository.GetTemplateById(templateId);

        return template is null ? null : LayoutMapper.ToDto(template.LayoutGrid);
    }

    public StyleRulesDto? GetStyleRules(Guid templateId)
    {
        var template = _templateRepository.GetTemplateById(templateId);

        return template is null ? null : LayoutMapper.ToDto(template.StyleRules);
    }
}
